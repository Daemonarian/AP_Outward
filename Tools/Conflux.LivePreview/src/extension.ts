import * as vscode from 'vscode';
import { spawn } from 'child_process';
import * as path from 'path';

let previewPanel: vscode.WebviewPanel | undefined = undefined;
let cliPath: string = '';

export function activate(context: vscode.ExtensionContext) {
    cliPath = path.join(context.extensionPath, 'bin', 'Conflux.exe');
    console.log("cliPath: " + cliPath);

    let disposable = vscode.commands.registerCommand('conflux-preview.showPreview', () => {
        if (previewPanel) {
            previewPanel.reveal(vscode.ViewColumn.Beside);
            return;
        }

        previewPanel = vscode.window.createWebviewPanel(
            'nodeCanvasPreview', 
            'Dialogue Preview', 
            vscode.ViewColumn.Beside, 
            { enableScripts: true }
        );

        previewPanel.onDidDispose(() => { previewPanel = undefined; });
        updatePreview();
    });

    context.subscriptions.push(disposable);

    let timeout: NodeJS.Timeout | undefined = undefined;
    vscode.workspace.onDidChangeTextDocument(event => {
        if (vscode.window.activeTextEditor && event.document === vscode.window.activeTextEditor.document) {
            if (timeout) {
                clearTimeout(timeout);
            }

            timeout = setTimeout(() => updatePreview(), 500);
        }
    });
}

function updatePreview() {
    if (!previewPanel || !vscode.window.activeTextEditor) {
        return;
    }

    const editorText = vscode.window.activeTextEditor.document.getText();
    
    const process = spawn(cliPath, ['--input', '-', '--output', '-', '--format', 'SVG']);

    process.on('error', (err) => {
        console.error('Failed to start CLI process:', err.message);
        if (previewPanel) {
            previewPanel.webview.html = `
                <div style="padding: 20px; color: red; font-family: sans-serif;">
                    <h2>Execution Error</h2>
                    <p>Could not start the C# tool. The executable was not found.</p>
                    <p><strong>Expected path:</strong> <code>${cliPath}</code></p>
                    <p><strong>Node Error:</strong> <code>${err.message}</code></p>
                </div>`;
        }
    });

    let svgOutput = '';
    let errorOutput = '';

    process.stdout.on('data', (data) => {
        svgOutput += data.toString();
    });

    process.stderr.on('data', (data) => {
        errorOutput += data.toString();
    });

    process.on('close', (code) => {
        if (!previewPanel) {
            return;
        }

        if (code === 0) {
            if (!svgOutput.trim()) {
                previewPanel.webview.html = `
                    <div style="padding: 20px; color: #856404; font-family: sans-serif;">
                        <h2>Warning: Empty Output (Code 0)</h2>
                        <p>The C# tool reported a successful run, but returned no SVG data.</p>
                        <p><strong>C# Debug Log / Stderr:</strong></p>
                        <pre style="white-space: pre-wrap; background: #fff3cd; padding: 10px;">${errorOutput || "No error output captured."}</pre>
                    </div>`;
            } else {
                previewPanel.webview.html = `
                    <!DOCTYPE html>
                    <html lang="en">
                    <head>
                        <style>
                            body { background-color: white; padding: 20px; display: flex; justify-content: center; }
                            svg { max-width: 100%; height: auto; }
                        </style>
                    </head>
                    <body>
                        ${svgOutput}
                    </body>
                    </html>`;
            }
        } else {
            previewPanel.webview.html = `
                <div style="padding: 20px; color: red; font-family: sans-serif;">
                    <h2>C# CLI Crash (Exit Code: ${code})</h2>
                    <p>The tool encountered an error processing this file:</p>
                    <pre style="white-space: pre-wrap; background: #ffe6e6; padding: 10px;">${errorOutput}</pre>
                </div>`;
        }
    });

    if (process.pid && process.stdin && !process.stdin.destroyed) {
        try {
            process.stdin.write(editorText);
            process.stdin.end();
        } catch (e) {
            console.error('Stream write error:', e);
        }
    } else {
        console.warn('Skipped writing to CLI: Process failed to start.');
    }
}

export function deactivate() {}