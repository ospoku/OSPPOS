document.addEventListener('DOMContentLoaded', () =>
{
    const selects = document.querySelectorAll('select.dmx-choices');
    selects.forEach(select => {
        const choices = new Choices(select,
            {
                removeItemButton: true,
                searchEnabled: true,
                itemSelectText: '',
                shouldSort: true,
                placeholder: true,
         
               
            });
    });
});    