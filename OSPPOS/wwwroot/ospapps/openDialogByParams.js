
function openDialogByParams(url, dialogId, userId) {
    const dialog = document.getElementById(dialogId);
    const contentDiv = document.getElementById(dialogId + "Content");

    if (!dialog || !contentDiv) {
        console.error("Dialog or content div not found for ID:", dialogId);
        return;
    }
    if (!url || !userId) {
        console.error("Missing URL or userId");
        return;
    }
    contentDiv.innerHTML = "<p>Loading...</p>";
    fetch(`${url}?Id=${encodeURIComponent(userId)}`)
        .then(res => res.text())
        .then(html => {
            contentDiv.innerHTML = html;
            dialog.showModal();
        })
        .catch(err => {
            contentDiv.innerHTML = "<p class='text-danger'>Failed to load content.</p>";
            console.error(err);
        });
}