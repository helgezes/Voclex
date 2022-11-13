const successModal = new bootstrap.Modal('#successfulCreation');

export function ShowSuccessfulCreationModal() {
    successModal.show();
}

export function DisposeModalAndGoToUrl(path) {
    successModal.dispose();
    location.href = location.origin + path;
}