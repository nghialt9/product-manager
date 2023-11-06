// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function select(targetId, sourceId) {
    var select = document.getElementById(sourceId);
    var index = select.selectedIndex ?? 0;
    document.getElementById(targetId).value = select.options[index].text;
}

function sellAdd(e, code, form, action) {
    e.preventDefault();
    var form = document.getElementById(form);
    form.action = "SellInvoiceDetail?code=" + code + "&action=" + action + "&subAction=1";
    form.method = "POST";
    form.submit();
}

function importAdd(e, code, form, action) {
    e.preventDefault();
    var form = document.getElementById(form);
    form.action = "ImportInvoiceDetail?code=" + code + "&action=" + action + "&subAction=1";
    form.method = "POST";
    form.submit();
}