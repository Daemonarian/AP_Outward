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