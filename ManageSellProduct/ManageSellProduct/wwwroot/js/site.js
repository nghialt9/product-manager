// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function select(targetId, sourceId) {
    var select = document.getElementById(sourceId);
    var index = select.selectedIndex ?? 0;
    debugger
    document.getElementById(targetId).value = select.options[index].text;
}