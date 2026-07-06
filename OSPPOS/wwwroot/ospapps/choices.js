/**
 * Choices.js Initialization Module
 * Manages all Choices instances globally with proper lifecycle management
 */

// Store all Choices instances for cleanup
const choicesInstances = [];

/**
 * Initialize Choices on a select element
 * @param {string|HTMLSelectElement} elementId - Element ID or HTMLSelectElement
 * @param {Object} customOptions - Optional custom configuration
 * @returns {Choices|null} The Choices instance or null if invalid
 */
function initializeChoices(elementId, customOptions = {}) {
    // If elementId is a string, find by ID
    let selectElement;
    if (typeof elementId === 'string') {
        selectElement = document.getElementById(elementId);
        if (!selectElement) {
            console.warn(`Element with ID "${elementId}" not found`);
            return null;
        }
    } else if (elementId && elementId.nodeType === 1) {
        // If it's a DOM element, use it directly
        selectElement = elementId;
    } else {
        console.warn('Invalid element reference');
        return null;
    }

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
        // Store with element ID for easy lookup
        if (selectElement.id) {
            choices._elementId = selectElement.id;
        }
        choicesInstances.push(choices);
        return choices;
    } catch (e) {
        console.error(`Error initializing Choices on element "${selectElement.id}":`, e);
        return null;
    }
}

/**
 * Initialize all select elements with the '.choices-select' class
 * @param {Object} options - Global options to apply
 * @param {string} containerId - Optional container ID to scope the search
 */
function initAllChoices(options = {}, containerId = null) {
    let selector = '.choices-select';
    let container = document;

    if (containerId) {
        container = document.getElementById(containerId);
        if (!container) {
            console.warn(`Container with ID "${containerId}" not found`);
            return 0;
        }
    }

    const selects = container.querySelectorAll(selector);
    selects.forEach(select => {
        initializeChoices(select, options);
    });
    return selects.length;
}

/**
 * Initialize Choices for a specific element by ID
 * @param {string} elementId - The ID of the select element
 * @param {Object} options - Custom options
 * @returns {Choices|null}
 */
function initChoicesById(elementId, options = {}) {
    return initializeChoices(elementId, options);
}

/**
 * Destroy a specific Choices instance
 * @param {string|HTMLSelectElement|Choices} target - Element ID, DOM element, or Choices instance to destroy
 */
function destroyChoices(target) {
    if (!target) return;

    let instance = target;

    // If it's a string, treat as element ID
    if (typeof target === 'string') {
        const element = document.getElementById(target);
        if (element && element._choicesInstance) {
            instance = element._choicesInstance;
        } else {
            console.warn(`Element with ID "${target}" not found or has no Choices instance`);
            return;
        }
    }

    // If it's a DOM element, get its instance
    if (target._choicesInstance) {
        instance = target._choicesInstance;
    }

    // If it's a Choices instance, destroy it
    if (instance && typeof instance.destroy === 'function') {
        try {
            const elementId = instance._elementId || (instance.element && instance.element.id);
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
            console.log(`Choices instance destroyed${elementId ? ` for element "${elementId}"` : ''}`);
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
    console.log('All Choices instances destroyed');
}

/**
 * Refresh Choices instance for a specific element
 * @param {string|HTMLSelectElement} elementId - Element ID or DOM element
 */
function refreshChoices(elementId) {
    let selectElement;
    if (typeof elementId === 'string') {
        selectElement = document.getElementById(elementId);
        if (!selectElement) {
            console.warn(`Element with ID "${elementId}" not found`);
            return;
        }
    } else if (elementId && elementId.nodeType === 1) {
        selectElement = elementId;
    } else {
        console.warn('Invalid element reference');
        return;
    }

    const instance = getChoicesInstance(selectElement);
    if (instance) {
        try {
            if (instance && typeof instance._store === 'object') {
                instance._store.clear();
                instance._renderItems();
                console.log(`Choices instance refreshed${selectElement.id ? ` for "${selectElement.id}"` : ''}`);
            }
        } catch (e) {
            console.warn('Error refreshing Choices instance:', e);
        }
    } else {
        console.warn('No Choices instance found for element');
    }
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
    console.log('All Choices instances refreshed');
}

/**
 * Get a Choices instance from a select element or ID
 * @param {string|HTMLSelectElement} elementId - Element ID or DOM element
 * @returns {Choices|null}
 */
function getChoicesInstance(elementId) {
    let selectElement;
    if (typeof elementId === 'string') {
        selectElement = document.getElementById(elementId);
        if (!selectElement) {
            console.warn(`Element with ID "${elementId}" not found`);
            return null;
        }
    } else if (elementId && elementId.nodeType === 1) {
        selectElement = elementId;
    } else {
        return null;
    }
    return selectElement?._choicesInstance || null;
}

/**
 * Get element ID from Choices instance
 * @param {Choices} instance - Choices instance
 * @returns {string|null}
 */
function getElementIdFromInstance(instance) {
    if (!instance) return null;
    return instance._elementId || (instance.element && instance.element.id) || null;
}

/**
 * Get all Choices instances in a container
 * @param {string} containerId - Container element ID
 * @returns {Array}
 */
function getChoicesInContainer(containerId) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.warn(`Container with ID "${containerId}" not found`);
        return [];
    }

    return choicesInstances.filter(instance => {
        if (!instance.element) return false;
        return container.contains(instance.element);
    });
}

// Export for use in other modules
if (typeof module !== 'undefined' && module.exports) {
    module.exports = {
        initializeChoices,
        initAllChoices,
        initChoicesById,
        destroyChoices,
        destroyAllChoices,
        refreshChoices,
        refreshAllChoices,
        getChoicesInstance,
        getElementIdFromInstance,
        getChoicesInContainer,
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
                        // Only initialize if it has an ID or we can generate one
                        if (select.id || select.getAttribute('data-auto-id')) {
                            initializeChoices(select);
                        } else {
                            // Generate a unique ID for dynamic elements
                            const uniqueId = `choices-${Date.now()}-${Math.random().toString(36).substr(2, 9)}`;
                            select.id = uniqueId;
                            initializeChoices(select);
                        }
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