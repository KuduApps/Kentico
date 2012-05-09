
function InsertSelectedItem(obj){
	var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
	if ((wopener) && (obj)) {
		var url = null;
		if ((obj.img_url) && (obj.img_url != '')) {
			url = createUrl(obj.img_url, obj.img_ext, obj.img_width, obj.img_height);
		}
		else if ((obj.av_url) && (obj.av_url != '')) {
			url = createUrl(obj.av_url, obj.av_ext, obj.av_width, obj.av_height);
		}
		else if ((obj.flash_url) && (obj.flash_url != '')) {
			url = createUrl(obj.flash_url, obj.flash_ext, obj.flash_width, obj.flash_height);
		}
		else if ((obj.url_url) && (obj.url_url != '')) {
			url = createUrl(obj.url_url, obj.url_ext, obj.url_width, obj.url_height);
		}
		if ((obj.editor_clientid != null) && (obj.editor_clientid != '')) {
			var editor = wopener.document.getElementById(obj.editor_clientid);
			if (editor != null) {
				if (url != null) {
					if (editor.value != null) {
						editor.value = url;
						if (editor.onchange) {
							editor.onchange();
						}
					}
				}
			}
		}
		else if (wopener.SetUrl != null) {
			wopener.SetUrl(url, obj.url_width, obj.url_height);
		}
	}
}

function GetSelectedItem(editorId){
	var obj = null;
	if ((editorId) && (editorId != '')) {
		var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
		if (wopener) {
			var editor = wopener.document.getElementById(editorId);
			if ((editor != null) && (editor.value) && (editor.value != '')) {
				obj = new Object();
				obj.url_url = editor.value;
				var ext = editor.value.match(/ext=([^&]*)/);
				if (ext) {
					obj.url_ext = ext[1];
				}
				var width = editor.value.match(/width=([^&]*)/);
				if (width) {
					obj.url_width = width[1];
				}
				var height = editor.value.match(/height=([^&]*)/);
				if (height) {
					obj.url_height = height[1];
				}
			}
		}
	}
	return obj;
}

function createUrl(url, ext, width, height){
	/*
	 var query = '';
	 // Create query string
	 if ((ext) && (ext != '')) {
	 query += "&ext=" + ext;
	 }
	 if ((width) && (width != '')) {
	 query += "&width=" + width;
	 }
	 if ((height) && (height != '')) {
	 query += "&height=" + height;
	 }
	 // Add query string into url
	 if (url.lastIndexOf('?') > 0) {
	 url = url + query;
	 }
	 else {
	 url = url + '?' + query.replace(/^&/, '');
	 }
	 */
	return url;
}
