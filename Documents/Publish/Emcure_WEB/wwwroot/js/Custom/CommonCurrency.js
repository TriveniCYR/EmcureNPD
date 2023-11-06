function formatCurrencyInElements(className) {
    const elements = document.querySelectorAll(`.${className}`);
    elements.forEach(function (element) {
        if (element.tagName === 'INPUT') {
            const formatInput = (inputElement) => {
                const inputText = inputElement.value;
                const hasNegativeSign = inputText.includes('-');
                const numericValue = parseFloat(inputText.replace(/[^\d.]/g, ''));
                if (!isNaN(numericValue)) {
                    inputElement.value = (hasNegativeSign ? '-' : '') + accounting.formatMoney(Math.abs(numericValue), { symbol: '', precision: 2 });
                }
            };
            formatInput(element);

            element.addEventListener('blur', function () {
                formatInput(this);
            });
        } else {
            const elementText = element.textContent;
            const hasNegativeSign = elementText.includes('-');
            const numericValue = parseFloat(elementText.replace(/[^\d.]/g, ''));
            if (!isNaN(numericValue)) {
                element.textContent = (hasNegativeSign ? '-' : '') + accounting.formatMoney(Math.abs(numericValue), { symbol: '', precision: 2 });
            }
        }
    });
}

function preventTextInCurrencyFields() {
    const currencyInputs = document.querySelectorAll('.format-currency');
    currencyInputs.forEach(function (inputElement) {
        inputElement.addEventListener('input', function () {
            this.value = this.value.replace(/[^0-9,-.]/g, '');

            const valueParts = this.value.split('.');
            if (valueParts.length > 2) {
                this.value = valueParts[0] + '.' + valueParts.slice(1).join('');
            }
        });
    });
}

$(document).ready(function () {
    formatCurrencyInElements('format-currency');
    preventTextInCurrencyFields();
});
