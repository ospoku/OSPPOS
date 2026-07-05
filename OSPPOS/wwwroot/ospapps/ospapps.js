
$(document).ready(function () {
    // Initialize DataTables for each table element
    $('.table').each(function () {
        const table = $(this).DataTable({
            "order": [[0, 'desc']]
        });

        // Function to update row numbers in the first column
        function updateRowNumbers() {
            let i = 1;
            table.cells(null, 0, { search: 'applied', order: 'applied' })
                .every(function () {
                    this.data(i++);
                });

            table.draw();
        }

        // Call updateRowNumbers on table initialization and whenever the table is sorted or searched
        updateRowNumbers();
        table.on('order.dt search.dt', updateRowNumbers);
    });
});









   



   




