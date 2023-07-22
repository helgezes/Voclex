let dotNetHelper;

export function InitializeLogoutPopover(dotNetHelperObj) {
    dotNetHelper = dotNetHelperObj;

    bootstrap.Popover.Default.allowList.button = [];
    const logoutPopoverTrigger = document.querySelector('#logout_popover');

    if (logoutPopoverTrigger === null) return;

    new bootstrap.Popover(logoutPopoverTrigger, {
        placement: 'bottom',
        html: true,
        content: '<button class="btn btn-danger" id="logout">Log out</button>'
    });

    document.addEventListener("shown.bs.popover", (event) => {
        if (event.srcElement.id !== "logout_popover") return;

        document.querySelector("#logout").addEventListener("click", logOut);
    });
}

async function logOut() {
    await dotNetHelper.invokeMethodAsync('LogOut');
    window.location.reload();
}

window.logOut = logOut;