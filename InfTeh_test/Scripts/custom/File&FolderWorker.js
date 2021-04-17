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

    //if (selectedElement != null) {
    //    return;
    //}

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
