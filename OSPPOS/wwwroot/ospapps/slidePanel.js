
    document.addEventListener('DOMContentLoaded', function () {
        document.querySelectorAll('[data-open]').forEach(button => {
            button.addEventListener('click', () => {
                const panelName = button.getAttribute('data-open');
                const panel = document.querySelector(`[data-panel="${panelName}"]`);
                panel.classList.toggle('open');
            });
        });

        document.querySelectorAll('[data-close]').forEach(button => {
            button.addEventListener('click', () => {
                const panelName = button.getAttribute('data-close');
                const panel = document.querySelector(`[data-panel="${panelName}"]`);
                panel.classList.remove('open');
            });
        });
    });
