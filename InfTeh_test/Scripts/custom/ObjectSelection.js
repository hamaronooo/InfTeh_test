
var selectedElement = null;

function setNavEventListener() {
    $("ul li > div").off("click");
    $("ul li > div").on("click", function () {
        select_li(this);
    });
}

function select_li(li) {
    var data_folder_id = li.getAttribute('data-folder-id');
    var data_file_id = li.getAttribute('data-file-id');

    var jq_li = null;

    if (data_folder_id != null)
        jq_li = $("#folder-container-" + data_folder_id);
    else if (data_file_id != null) 
        jq_li = $('#file-container-' + data_file_id);
    
    if (jq_li != null && jq_li.hasClass("selectedElement")) {
        selectedElement.removeClass("selectedElement");
        selectedElement = null;
        $('#explorer_header_path').html('');
        return;
    }

    $('[data-folder-id], [data-file-id]').removeClass("selectedElement");

    selectedElement = jq_li;
    if (jq_li != null && !jq_li.next('ul').hasClass('show')) {
        selectedElement.addClass("selectedElement");
    }

    displayFilePath();
    loadUploadForm();
}

function getParentFolderID() {
    if (selectedElement == null) { return null; }

    var data_folder_id = selectedElement.attr('data-folder-id');
    var data_file_id = selectedElement.attr('data-file-id');
    console.log(selectedElement);
    console.log(data_folder_id);
    console.log(data_file_id);

    if (data_folder_id != null) {
        console.log(selectedElement.attr('data-folder-id'));
        return selectedElement.attr('data-folder-id');
    }
    else if (data_file_id != null) {
        return selectedElement.attr('data-parent-folder-id');
    }
    else {
        return null;
    }
}

function displayFilePath() {
    var currentFolderId = getParentFolderID();
    $('#explorer_header_path').load('/Explorer/_PartialFilePathBlock?currentFolderid=' + currentFolderId);
}
function loadUploadForm() {
    var currentFolderId = getParentFolderID();
    $('#explorer_filedrop_container').html('');
    $('#explorer_filedrop_container').load('/Upload/_PartialFiledrop?mode=1&folderid=' + currentFolderId);
}