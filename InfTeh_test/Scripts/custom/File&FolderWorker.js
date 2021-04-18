var OpenedFileID = null;
function deleteFolder(folderid) {
    showPreloader();
    $.ajax({
        url: '/Folder/_PartialDeleteFolderForm',
        data: {
            folderid: folderid
        },
        success: function (html) {
            $('#ModalBody').html(html);
        }
    }).always(function () {
        hidePreloader();
    });
}
function renameFolder(folderid) {
    showPreloader();
    $.ajax({
        url: '/Folder/_PartialRenameFolderForm',
        data: {
            folderid: folderid
        },
        success: function (html) {
            $('#ModalBody').html(html);
        }
    }).always(function () {
        hidePreloader();
    });
}
function deleteFile(fileid) {
    showPreloader();

    $.ajax({
        url: '/File/_PartialDeleteFileForm',
        data: {
            fileid: fileid
        },
        success: function (html) {
            $('#ModalBody').html(html);
        }
    }).always(function () {
        hidePreloader();
    });
}
function showFileData(fileid) {
    setPreloader($('#explorer_content_container'));
    OpenedFileID = fileid;
    $.ajax({
        method: "POST",
        url: '/File/ShowFileData',
        data: {
            fileid: fileid
        },
        success: function (html) {
            $('#explorer_content_container').html(html);
        }
    });
}
function showFolderContent(folderid) {
    setPreloader($('#explorer_content_container'));

    $.ajax({
        method: "POST",
        url: '/Explorer/_PartialFolderContent',
        data: {
            folderid: folderid
        },
        success: function (html) {
            $('#explorer_content_container').html(html);
        }
    });
}
function createFolderForm() {
    $('#createFolderForm').remove();
    $.ajax({
        type: "GET",
        url: '/Folder/_PartialCreateFolderForm',
        data: {
            parent_folder: getParentFolderID()
        },
        success: function (html) {
            if (selectedElement != null) {
                if (selectedElement.attr('data-file-id') != null) 
                    $('#navAccordion').prepend('<li>' + html + '</li>');
                selectedElement.next().prepend(html);
            }
            else {
                $('#navAccordion').prepend('<li>' + html + '</li>');
            }
        }
    });
}


// icons
function loadIconCreateForm() {
    showPreloader();
    $.ajax({
        type: "GET",
        url: '/FileExtension/_PartialIconCreateForm',
        success: function (html) {
            $('#explorer_content_container').html(html);
            loadIconUploadForm();
        }
    }).always(function () {
        hidePreloader();
    });
}
function loadIconUploadForm() {
    $('#explorer_filedrop_container').load('/Upload/_PartialFiledrop?mode=2');
}
