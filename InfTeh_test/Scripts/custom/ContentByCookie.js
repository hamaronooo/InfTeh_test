$(document).ready(function() {
    OpenCookieUl();
});

function OpenCookieUl() {
    var firstTimeVisitCookie = $.cookie('firstTimeVisitCookie');

    if (firstTimeVisitCookie !== null) {
        if (firstTimeVisitCookie !== 'visited') {
            setPreloader($('#explorer_content_container'));
            $('#explorer_content_container').load('/Home/_PartialProjectInfo');
            $.cookie('firstTimeVisitCookie', 'visited');
        }
    }
}