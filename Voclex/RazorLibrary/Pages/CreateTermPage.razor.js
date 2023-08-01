const successModal = new bootstrap.Modal('#successfulCreation');

export function ShowSuccessfulCreationModal() {
    successModal.show();
}

window.HideSuccessfulCreationModal = function HideSuccessfulCreationModal() {
    successModal.hide();
}