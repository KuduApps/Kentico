/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.editorConfig = function(config) {

    config.extraPlugins = 'CMSPlugins';
    config.uiColor = '#eeeeee';
    config.skin = 'kentico';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.entities_latin = false;
    config.protectedSource.push(/<script[\s\S]*?<\/script>/gi);   // <SCRIPT> tags.

    config.toolbar_Full = config.toolbar_Default =
    [
        ['Source', '-'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'SpellChecker', 'Scayt', '-'],
        ['Undo', 'Redo', 'Find', 'Replace', 'RemoveFormat', '-'],
        ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-'],
        ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv', '-'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
        ['InsertLink', 'Unlink', 'Anchor', '-'],
        ['InsertImageOrMedia', 'QuicklyInsertImage', 'Table', 'HorizontalRule', 'SpecialChar', '-'],
        ['InsertForms', 'InsertInlineControls', 'InsertPolls', 'InsertRating', 'InsertYouTubeVideo', 'InsertWidget'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor', '-'], 
        ['Maximize', 'ShowBlocks']
    ];

    config.toolbar_Wireframe =
    [
	    ['Cut', 'Copy', 'PasteText', '-'],
	    ['Undo', 'Redo', 'Find', 'Replace', 'RemoveFormat', '-'],
	    ['Bold', 'Italic', 'Underline', 'Strike', '-'],
	    ['NumberedList', 'BulletedList', '-'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
	    ['InsertLink', 'Unlink', '-'],
	    ['InsertImageOrMedia', 'QuicklyInsertImage', 'Table', '-'],
	    ['Format']
    ];

    config.toolbar_Basic =
    [
	    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'InsertLink', 'Unlink']
    ];

    config.toolbar_ProjectManagement =
    [
	    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'RemoveFormat', '-', 'NumberedList', 'BulletedList', '-', 'TextColor', 'BGColor', 'Maximize']
    ];

    config.toolbar_BizForm = [
	    ['Source', '-'],
	    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-'],
	    ['Undo', 'Redo', 'Find', 'Replace', 'RemoveFormat', '-'],
	    ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-'],
	    ['NumberedList', 'BulletedList', 'Outdent', 'Indent', '-'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
	    ['InsertLink', 'Unlink', 'Anchor', '-'],
	    ['Table', 'HorizontalRule', 'SpecialChar'],
	    '/',
	    ['Styles', 'Format', 'Font', 'FontSize'],
	    ['TextColor', 'BGColor', '-'],
        ['Maximize']
    ];

    config.toolbar_Forum = [
	    ['Bold', 'Italic', '-', 'InsertLink', 'InsertUrl', 'InsertImageOrMedia', 'InsertImage', 'InsertQuote', '-', 'NumberedList', 'BulletedList', '-', 'TextColor', 'BGColor']
    ];

    config.toolbar_Newsletter = config.toolbar_Reporting = [
	    ['Source', '-'],
	    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-'],
	    ['Undo', 'Redo', 'Find', 'Replace', 'RemoveFormat', '-'],
	    ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-'],
	    ['NumberedList', 'BulletedList', 'Outdent', 'Indent', '-'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
	    ['InsertLink', 'Unlink', 'Anchor', '-'],
	    ['InsertImageOrMedia', 'QuicklyInsertImage', 'Table', 'HorizontalRule', 'SpecialChar'],
	    '/',
	    ['Styles', 'Format', 'Font', 'FontSize'],
	    ['TextColor', 'BGColor', '-'],
	    ['Maximize']
    ];

    config.toolbar_SimpleEdit = [
	    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-'],
	    ['Undo', 'Redo', 'Find', 'Replace', 'RemoveFormat', '-'],
	    ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-'],
	    ['NumberedList', 'BulletedList', 'Outdent', 'Indent', '-'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
	    ['InsertLink', 'Unlink', 'Anchor', '-'],
	    ['InsertImageOrMedia', 'QuicklyInsertImage', 'Table', 'HorizontalRule', 'SpecialChar'],
	    '/',
	    ['Styles', 'Format', 'Font', 'FontSize'],
	    ['TextColor', 'BGColor', '-'],
	    ['Maximize']
    ];

    config.toolbar_Invoice = [
        ['Source', '-'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', 'SpellChecker', 'Scayt', '-'],
        ['Undo', 'Redo', 'Find', 'Replace', 'RemoveFormat', '-'],
        ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-'],
        ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv', '-'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
        ['InsertImageOrMedia', 'Table', 'HorizontalRule', 'SpecialChar'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor', '-'],
        ['Maximize', 'ShowBlocks']
    ];

    config.toolbar_Group = [
	    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'InsertLink', 'Unlink', 'InsertGroupPolls']
    ];

    config.toolbar_Widgets = [
	    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'InsertLink', 'Unlink', 'InsertImageOrMedia'],
        '/',
	    ['Format', 'Font', 'FontSize'],
	    ['TextColor', 'BGColor']
 ];

    config.toolbar_Disabled = [
        ['Maximize']
    ];

    config.scayt_customerid = '1:vhwPv1-GjUlu4-PiZbR3-lgyTz1-uLT5t-9hGBg2-rs6zY-qWz4Z3-ujfLE3-lheru4-Zzxzv-kq4';
};
