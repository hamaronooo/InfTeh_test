// на всю страницу
function showPreloader() {
    $('.custom_preloader').remove();
    var Content = '<div class="custom_preloader d-flex justify-content-center">' +
        '<div class="spinner-border text-white" style="margin-top:40vh;width: 5rem; height: 5rem;"' +
        'role="status">' +
        '<span class="sr-only">Loading...</span>' +
        '</div>' +
        '</div>';

    $('html').prepend(Content);
}

function hidePreloader() {
    setTimeout(function() {
        $('.custom_preloader').remove();
    }, 300);
}

// в контейнере
function setPreloader(jqElement) {
    var Content = '<div class="d-flex justify-content-center">' +
        '<div class="spinner-border text-white"' +
        'role="status">' +
        '<span class="sr-only">Loading...</span>' +
        '</div>' +
        '</div>';
    jqElement.html(Content);
}

// над папкой
function setFolderPreloader(folderid) {
    var Content = '<div class="d-flex justify-content-center">' +
        '<div class="spinner-grow text-warning"' +
        'role="status">' +
        '<span class="sr-only">Loading...</span>' +
        '</div>' +
        '</div>';
    $('#folder_preloader_' + folderid).html(Content);
}

function removeFolderPreloader(folderid) {
    $('#folder_preloader_' + folderid).html('');
}