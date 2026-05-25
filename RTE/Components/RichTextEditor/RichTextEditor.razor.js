export async function init(elementId, value, config, dotNetReference) {
    if(document.getElementById(elementId)) {
        destroy(elementId);
    }

    console.log("initializing editor for element", elementId)
    hugerte.init({
        selector: `#${elementId}`,
        base_url: "/lib/HugeRTE",
        suffix: ".min",
        forced_root_block: "div",
        ...(config ?? {}),
        setup(editor) {
            editor.on("init", () => {
                editor.setContent(value ?? "");
            });

            editor.on("change input undo redo", () => {
                dotNetReference.invokeMethodAsync("SetValueFromJs", editor.getContent());
            });
        }
    });
}

export function getContent(elementId) {
    const editor = hugerte.get(elementId);
    return editor ? editor.getContent() : "";
}

export function setContent(elementId, content) {
    const editor = hugerte.get(elementId);

    if (editor) {
        editor.setContent(content ?? "");
    }
}

export function destroy(elementId) {
    const editor = hugerte.get(elementId);

    if (editor) {
        console.log("deleting element")
        editor.remove();
    }
}