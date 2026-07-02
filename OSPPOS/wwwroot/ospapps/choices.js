
// Initialize Choices.js for all select elements in the dialog
document.addEventListener('DOMContentLoaded', function () {
    // Only initialize if Choices is available
    if (typeof Choices !== 'undefined') {
        const selects = document.querySelectorAll('.choices-select');
        selects.forEach(select => {
            new Choices(select, {
                searchEnabled: true,
                searchPlaceholderValue: 'Search...',
                placeholder: true,
                placeholderValue: 'Select option...',
                shouldSort: false,
                itemSelectText: '',
                noResultsText: 'No options found',
                noChoicesText: 'No options available',
                classNames: {
                    containerOuter: 'choices choices-dark',
                    containerInner: 'choices__inner choices__inner-dark',
                    input: 'choices__input choices__input-dark',
                    inputCloned: 'choices__input--cloned',
                    list: 'choices__list',
                    listItems: 'choices__list--multiple',
                    listSingle: 'choices__list--single',
                    listDropdown: 'choices__list--dropdown',
                    item: 'choices__item',
                    itemSelectable: 'choices__item--selectable',
                    itemDisabled: 'choices__item--disabled',
                    itemChoice: 'choices__item--choice',
                    placeholder: 'choices__placeholder',
                    group: 'choices__group',
                    groupHeading: 'choices__heading',
                    button: 'choices__button',
                    activeState: 'is-active',
                    focusState: 'is-focused',
                    openState: 'is-open',
                    disabledState: 'is-disabled',
                    highlightedState: 'is-highlighted',
                    selectedState: 'is-selected',
                    flippedState: 'is-flipped',
                    loadingState: 'is-loading',
                    noResults: 'has-no-results',
                    noChoices: 'has-no-choices'
                }
            });
        });
    }
});