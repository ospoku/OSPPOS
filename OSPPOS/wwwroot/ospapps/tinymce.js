(function () {
    if (typeof tinymce === "undefined") {
        console.error("TinyMCE not loaded.");
        return;
    }

    // Make sure every textarea has an ID
    function ensureIds() {
        document.querySelectorAll("textarea.dmx-textarea").forEach((ta) => {
            if (!ta.id) {
                ta.id = "dmx-textarea-" + Math.random().toString(36).slice(2, 9);
            }
        });
    }

    // Initialize editor for a given textarea
    function initEditor(textarea) {
        if (!textarea || !textarea.id) return;

        // Skip if already initialized
        if (tinymce.get(textarea.id)) return;

        tinymce.init({
            target: textarea,
            height: 300,
            menubar: false,
            plugins: "lists link image code",
            toolbar: "undo redo | bold italic underline | bullist numlist | link image | code",
            branding: false,
            setup: (editor) => {
                const dialog = textarea.closest("dialog");
                if (dialog) {
                    dialog.addEventListener("close", () => {
                        if (tinymce.get(editor.id)) {
                            tinymce.get(editor.id).remove();
                        }
                    }, { once: true });
                }
            },
        });
    }

    // Initialize editors in a dialog
    function initEditorsInDialog(dialog) {
        if (!dialog) return;
        ensureIds();
        const textareas = dialog.querySelectorAll("textarea.dmx-textarea");
        textareas.forEach(initEditor);
    }

    // Observe dialog open/close events
    function observeDialogs() {
        const dialogObserver = new MutationObserver((mutations) => {
            mutations.forEach((m) => {
                const target = m.target;
                if (!(target instanceof HTMLDialogElement)) return;

                if (target.open) {
                    // dialog opened
                    initEditorsInDialog(target);
                } else {
                    // dialog closed, remove editors
                    const editorsToRemove = tinymce.editors.filter((e) =>
                        target.contains(e.targetElm)
                    );
                    editorsToRemove.forEach((ed) => ed.remove());
                }
            });
        });

        // Observe all dialogs for attribute changes
        document.querySelectorAll("dialog").forEach((dialog) => {
            dialogObserver.observe(dialog, { attributes: true, attributeFilter: ["open"] });
        });
    }

    document.addEventListener("DOMContentLoaded", () => {
        ensureIds();
        observeDialogs();

        // initialize editors not in dialogs (if any)
        document.querySelectorAll("textarea.dmx-textarea").forEach((ta) => {
            if (!ta.closest("dialog")) initEditor(ta);
        });

        console.log("✅ TinyMCE Dialog Initializer Ready");
    });
})();
