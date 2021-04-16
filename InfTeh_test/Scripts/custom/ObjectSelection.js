
var selectedElement = null;

function setNavEventListener() {
    $("ul li > div").off("click");
    $("ul li > div").on("click", function () {
        select_li(this);
    });
}

function select_li(li) {
    console.log('CALLER:  ' + select_li.caller);
    var data_folder_id = li.getAttribute('data-folder-id');
    var data_file_id = li.getAttribute('data-file-id');

    var jq_li = null;

    if (data_folder_id != null) {
        console.log("folder-container-" + data_folder_id);
        jq_li = $("#folder-container-" + data_folder_id);
    }
    else if (data_file_id != null) {
        console.log("file-container-" + data_file_id);
        jq_li = $('#file-container-' + data_file_id);
    } else {
        console.log('selected element: ' + selectedElement);

    }


    if (jq_li != null && jq_li.hasClass("selectedElement")) {
        console.log('has class');
        selectedElement.removeClass("selectedElement");
        selectedElement = null;
        $('#explorer_header_container').html('');
        return;
    }

    $('[data-folder-id], [data-file-id]').removeClass("selectedElement");

    selectedElement = jq_li;
    if (jq_li != null)
        selectedElement.addClass("selectedElement");

    displayFilePath(getParentFolderID());
}

function getParentFolderID() {
    if (selectedElement == null) { return null; }

    var data_folder_id = selectedElement.attr('data-folder-id');
    var data_file_id = selectedElement.attr('data-file-id');

    if (data_folder_id != null) {
        return selectedElement.attr('data-folder-id');
    }
    else if (data_file_id != null) {
        return selectedElement.attr('data-parent-folder-id');
    }
    else {
        return null;
    }
}

function displayFilePath(currentFolderID) {
    $('#explorer_header_container').load('/Explorer/_PartialFilePathBlock?currentFolderid=' + currentFolderID);
}