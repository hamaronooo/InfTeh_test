
function SaveState(id) {
    $.cookie('opened_ul_id', id, { path: '/' });
}

function OpenCookieUl() {
    var openedUlCookie = $.cookie('opened_ul_id');

    if (openedUlCookie !== null) {
        if (openedUlCookie !== 'navAccordion') {
            $('#' + openedUlCookie).attr('class', 'nav-second-level collapse show');
            $('#' + openedUlCookie).prev().attr('class', 'nav-link nav-link-collapse nav-link-show text-white');

            var parents = $('#' + openedUlCookie).parents('ul');

            $.each(parents, function (index, value) {
                if (value.id !== 'navAccordion') {
                    $('#' + value.id).attr('class', 'nav-second-level collapse show');
                    $('#' + value.id).prev().attr('class', 'nav-link nav-link-collapse nav-link-show text-white');
                }
            });
        }
    }
}