
var selectedElement = null;

function setNavEventListener() {
    $("ul li div").off("click").on("click", function () {
        select_li(this);
    });
}

function select_li(li) {
    $('[data-folder-id], [data-file-id]').removeClass("selectedElement");

    var data_folder_id = li.getAttribute('data-folder-id');
    var data_file_id = li.getAttribute('data-file-id');

    if (data_folder_id != null) {
        console.log("folder-container-" + data_folder_id);
        selectedElement = $("#folder-container-" + data_folder_id);
    }
    else if (data_file_id != null) {
        console.log("file-container-" + data_file_id);
        selectedElement = $('#file-container-' + data_file_id);
    }
    console.log('selected element: '+selectedElement);
    selectedElement.addClass("selectedElement");
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