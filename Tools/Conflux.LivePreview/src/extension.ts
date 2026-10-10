import * as vscode from 'vscode';
import { spawn } from 'child_process';
import * as path from 'path';

let cliPath: string = '';

const activePreviews = new Map<string, vscode.WebviewPanel>();
const activeTimeouts = new Map<string, NodeJS.Timeout>();

export function activate(context: vscode.ExtensionContext) {
    cliPath = path.join(context.extensionPath, 'bin', 'Conflux.exe');

    let previewCommand = vscode.commands.registerCommand('conflux-preview.showPreview', () => {
        const editor = vscode.window.activeTextEditor;
        if (!editor || (editor.document.languageId !== 'yaml' && editor.document.languageId !== 'json'))
        {
            return;
        }

        const documentUri = editor.document.uri.toString();
        if (activePreviews.has(documentUri)) {
            activePreviews.get(documentUri)?.reveal(vscode.ViewColumn.Beside);
            return;
        }

        const panel = vscode.window.createWebviewPanel(
            'confluxPreview',
            `Preview: ${path.basename(editor.document.fileName)}`,
            vscode.ViewColumn.Beside,
            { enableScripts: true}
        );

        activePreviews.set(documentUri, panel);

        panel.onDidDispose(() => {
            activePreviews.delete(documentUri);
        });

        updatePreview(panel, editor.document);
    });

    context.subscriptions.push(previewCommand);

    vscode.workspace.onDidChangeTextDocument(event => {
        const documentUri = event.document.uri.toString();
        if (activePreviews.has(documentUri)) {
            const panel = activePreviews.get(documentUri)!;

            if (activeTimeouts.has(documentUri)) {
                const oldTimeout = activeTimeouts.get(documentUri)!;
                clearTimeout(oldTimeout);
                activeTimeouts.delete(documentUri);
            }

            const timeout = setTimeout(() => updatePreview(panel, event.document), 500);
            activeTimeouts.set(documentUri, timeout);
        }
    });

    vscode.workspace.onDidDeleteFiles(event => {
        for (const uri of event.files) {
            const uriString = uri.toString();
            if (activePreviews.has(uriString)) {
                activePreviews.get(uriString)?.dispose();
            }
        }
    });
}

function updatePreview(panel: vscode.WebviewPanel, document: vscode.TextDocument) {
    if (!panel || !vscode.window.activeTextEditor) {
        return;
    }

    const editorText = document.getText();
    
    const process = spawn(cliPath, ['--input', '-', '--output', '-', '--format', 'SVG']);

    process.on('error', (err) => {
        console.error('Failed to start CLI process:', err.message);
        if (panel) {
            panel.webview.html = `
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
        if (!panel) {
            return;
        }

        if (code === 0) {
            if (!svgOutput.trim()) {
                panel.webview.html = `
                    <div style="padding: 20px; color: #856404; font-family: sans-serif;">
                        <h2>Warning: Empty Output (Code 0)</h2>
                        <p>The C# tool reported a successful run, but returned no SVG data.</p>
                        <p><strong>C# Debug Log / Stderr:</strong></p>
                        <pre style="white-space: pre-wrap; background: #fff3cd; padding: 10px;">${errorOutput || "No error output captured."}</pre>
                    </div>`;
            } else {
                panel.webview.html = `
                    <!DOCTYPE html>
                    <html lang="en">
                    <head>
                        <style>
                            body {
                                width: 100%;
                                height: 100%;
                                margin: 0;
                                padding: 0;
                                overflow: hidden;
                                background-color: white; 
                            }

                            svg {
                                width: 100vw;
                                height: 100vh;
                                /*
                                max-width: 100%;
                                max-height: 100%;
                                */
                                display: block;
                            }
                        </style>
                        
                        <script src="https://cdn.jsdelivr.net/npm/svg-pan-zoom@3.6.1/dist/svg-pan-zoom.min.js"></script>
                        <script>
                            window.onload = function() {
                                const svg = document.querySelector('svg');
                                if (svg) {
                                    const panZoom = svgPanZoom(svg, {
                                        zoomEnabled: true,
                                        controlIconsEnabled: true,
                                        fit: true,
                                        center: true,
                                        minZoom: 1,
                                        maxZoom: 1.5,
                                        zoomScaleSensitivity: 0.5,
                                        preventMouseEventsDefault: false
                                    });

                                    const fitZoomLevel = panZoom.getZoom();
                                    panZoom.setMinZoom(fitZoomLevel);

                                    let nativeWidth = 0;
                                    let nativeHeight = 0;
                                    if (svg.hasAttribute('viewBox')) {
                                        const viewBox = svg.getAttribute('viewBox').split(' ');
                                        nativeWidth = parseFloat(viewBox[2]);
                                        nativeHeight = parseFloat(viewBox[3]);
                                    } else if (svg.hasAttribute('width') || svg.hasAttribute('height')) {
                                        if (svg.hasAttribute('width')) {
                                            const rawWidth = svg.getAttribute('width');
                                            nativeWidth = parseFloat(rawWidth);
                                            if (rawWidth.includes('pt')) {
                                                nativeWidth = nativeWidth * 1.3333;
                                            }
                                        }
                                        if (svg.hasAttribute('height')) {
                                            const rawHeight = svg.getAttribute('height');
                                            nativeHeight = parseFloat(rawHeight);
                                            if (rawHeight.includes('pt')) {
                                                nativeHeight = nativeHeight * 1.3333;
                                            }
                                        }
                                    }

                                    let maxZoomLevelWidth = 0;
                                    if (nativeWidth > 0) {
                                        const screenWidth = svg.getBoundingClientRect().width;
                                        maxZoomLevelWidth = nativeWidth / screenWidth;
                                    }

                                    let maxZoomLevelHeight = 0;
                                    if (nativeHeight > 0) {
                                        const screenHeight = svg.getBoundingClientRect().height;
                                        maxZoomLevelHeight = nativeHeight / screenHeight;
                                    }

                                    let maxZoomLevel = Math.max(2 * maxZoomLevelWidth, 2 * maxZoomLevelHeight, 2 * fitZoomLevel);
                                    panZoom.setMaxZoom(maxZoomLevel);

                                    svg.querySelectorAll('text').forEach(textEl => {
                                        textEl.style.cursor = 'text';
                                        textEl.addEventListener('mousedown', (e) => {
                                            e.stopPropagation();
                                        });
                                    });
                                }
                            };
                        </script>
                    </head>
                    <body>
                        ${svgOutput}
                    </body>
                    </html>`;
            }
        } else {
            panel.webview.html = `
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