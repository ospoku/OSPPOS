document.addEventListener('DOMContentLoaded', () => {
    const cells = document.querySelectorAll('#dataTable td.ellipsed-text');
    cells.forEach(cell => {
        if (cell.scrollWidth > cell.clientWidth) {
            cell.title = cell.textContent; // Show full text on hover
        }
    });
});