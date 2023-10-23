function formatCurrencyInElements(className) {
    const elements = document.querySelectorAll(`.${className}`);
    elements.forEach(function (element) {
        if (element.tagName === 'INPUT') {
            const formatInput = (inputElement) => {
                const numericValue = parseFloat(inputElement.value.replace(/[^\d.]/g, ''));
                if (!isNaN(numericValue)) {
                    inputElement.value = accounting.formatMoney(numericValue, { symbol: '', precision: 2 });
                }
            };
            formatInput(element);

            element.addEventListener('blur', function () {
                formatInput(this);
            });
        } else {
            const numericValue = parseFloat(element.textContent.replace(/[^\d.]/g, ''));
            if (!isNaN(numericValue)) {
                element.textContent = accounting.formatMoney(numericValue, { symbol: '', precision: 2 });
            }
        }
    });
}

function preventTextInCurrencyFields() {
    const currencyInputs = document.querySelectorAll('.format-currency');
    currencyInputs.forEach(function (inputElement) {
        inputElement.addEventListener('input', function () {
            this.value = this.value.replace(/[^0-9,.]/g, '');

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
