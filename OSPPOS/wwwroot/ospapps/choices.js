/**
 * Choices.js Initialization Module
 * Manages all Choices instances globally with proper lifecycle management
 */

// Store all Choices instances for cleanup
const choicesInstances = [];

/**
 * Initialize Choices on a select element
 * @param {HTMLSelectElement} selectElement - The select to enhance
 * @param {Object} customOptions - Optional custom configuration
 * @returns {Choices|null} The Choices instance or null if invalid
 */
function initializeChoices(selectElement, customOptions = {}) {
    if (!selectElement || typeof Choices === 'undefined') {
        console.warn('Choices.js not available or invalid element');
        return null;
    }

    // Destroy existing instance if present
    if (selectElement._choicesInstance) {
        try {
            selectElement._choicesInstance.destroy();
        } catch (e) {
            console.warn('Error destroying existing Choices instance:', e);
        }
    }

    // Default configuration
    const defaultOptions = {
        searchEnabled: true,
        searchPlaceholderValue: 'Search...',
        placeholder: true,
        placeholderValue: selectElement.dataset.placeholder || 'Select option...',
        shouldSort: false,
        itemSelectText: '',
        noResultsText: selectElement.dataset.noResults || 'No options found',
        noChoicesText: selectElement.dataset.noChoices || 'No options available',
        searchResultLimit: 20,
        position: 'bottom',
        classNames: {
            containerOuter: 'choices',
            containerInner: 'choices__inner',
            input: 'choices__input',
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
    };

    // Merge with custom options
    const options = { ...defaultOptions, ...customOptions };

    // Create and store instance
    try {
        const choices = new Choices(selectElement, options);
        selectElement._choicesInstance = choices;
        choicesInstances.push(choices);
        return choices;
    } catch (e) {
        console.error('Error initializing Choices:', e);
        return null;
    }
}

/**
 * Initialize all select elements with the '.choices-select' class
 * @param {Object} options - Global options to apply
 */
function initAllChoices(options = {}) {
    const selects = document.querySelectorAll('.choices-select');
    selects.forEach(select => {
        initializeChoices(select, options);
    });
    return selects.length;
}

/**
 * Destroy a specific Choices instance
 * @param {HTMLSelectElement|Choices} target - Element or Choices instance to destroy
 */
function destroyChoices(target) {
    if (!target) return;

    let instance = target;

    // If it's a DOM element, get its instance
    if (target._choicesInstance) {
        instance = target._choicesInstance;
    }

    // If it's a Choices instance, destroy it
    if (instance && typeof instance.destroy === 'function') {
        try {
            instance.destroy();
            // Remove from tracking array
            const index = choicesInstances.indexOf(instance);
            if (index > -1) {
                choicesInstances.splice(index, 1);
            }
            // Clean up reference
            if (instance.element && instance.element._choicesInstance) {
                instance.element._choicesInstance = null;
            }
        } catch (e) {
            console.warn('Error destroying Choices instance:', e);
        }
    }
}

/**
 * Destroy ALL Choices instances
 */
function destroyAllChoices() {
    // Clone array to avoid modification during iteration
    const instances = [...choicesInstances];
    instances.forEach(instance => {
        try {
            instance.destroy();
        } catch (e) {
            console.warn('Error destroying Choices instance:', e);
        }
    });
    // Clear the array
    choicesInstances.length = 0;
}

/**
 * Refresh all Choices instances (useful after dynamic updates)
 */
function refreshAllChoices() {
    choicesInstances.forEach(instance => {
        try {
            if (instance && typeof instance._store === 'object') {
                instance._store.clear();
                instance._renderItems();
            }
        } catch (e) {
            console.warn('Error refreshing Choices instance:', e);
        }
    });
}

/**
 * Get a Choices instance from a select element
 * @param {HTMLSelectElement} selectElement 
 * @returns {Choices|null}
 */
function getChoicesInstance(selectElement) {
    return selectElement?._choicesInstance || null;
}

// Export for use in other modules
if (typeof module !== 'undefined' && module.exports) {
    module.exports = {
        initializeChoices,
        initAllChoices,
        destroyChoices,
        destroyAllChoices,
        refreshAllChoices,
        getChoicesInstance,
        choicesInstances
    };
}

// Auto-initialize when DOM is ready
document.addEventListener('DOMContentLoaded', function () {
    initAllChoices();
});

// Handle dynamic content - observe DOM changes
if (window.MutationObserver) {
    const observer = new MutationObserver((mutations) => {
        mutations.forEach((mutation) => {
            // Check for added nodes
            mutation.addedNodes.forEach((node) => {
                // If it's an element with .choices-select class
                if (node.nodeType === 1 && node.matches && node.matches('.choices-select')) {
                    initializeChoices(node);
                }
                // Check children of added nodes
                if (node.querySelectorAll) {
                    node.querySelectorAll('.choices-select').forEach(select => {
                        initializeChoices(select);
                    });
                }
            });
        });
    });

    // Start observing when DOM is ready
    document.addEventListener('DOMContentLoaded', function () {
        observer.observe(document.body, {
            childList: true,
            subtree: true
        });
    });
}

// Cleanup on page unload (optional)
window.addEventListener('beforeunload', function () {
    destroyAllChoices();
});