function UP_Init(id, time) {
    if (!window.noProgress) {
        var async = false;
        try {
            async = Sys.WebForms.PageRequestManager.getInstance()._postBackSettings.async;
        }
        catch (e) {
        }
        if (!async) {
            setTimeout("document.getElementById('" + id + "').style.display = 'inline'", time);
        }
    }
    return true;
}
