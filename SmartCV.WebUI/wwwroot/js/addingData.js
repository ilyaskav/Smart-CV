// Полезные ссылки
//  http://htmltojavascript.com/
// https://www.sitepoint.com/16-jquery-selectboxdrop-down-plugins/

// НАЧАЛО Общие для всех функции
// доставание managerId из URI
var getManagerIdFromURI = function () {
    var sPageURL, managerId = 0;
    if (window.location.search == "") {
        sPageURL = decodeURIComponent(window.location.pathname);
        managerId = sPageURL.substring(sPageURL.lastIndexOf('/') + 1, sPageURL.length);
    }
    else {
        sPageURL = decodeURIComponent(window.location.search);
        managerId = sPageURL.substring(sPageURL.lastIndexOf('=') + 1, sPageURL.length);
    }
    return managerId;
};

// разделение секций
var separate = function (selector) {
    var element = $(selector);
    if (element == undefined || element.size() == 0) return;

    element.after('<hr/>');
}

// КОНЕЦ

$(function () {

    // добавление поля для личного качества
    $('#addPersonalQuality').on('click', function () {
        var html = [
            '            <div class="personalQuality">',
            '                <div class="col-md-9 col-md-offset-2">',
            '                    <input type="text" class="form-control" name="name" />',
            '                </div>',
            '                <div class="col-md-1 delete-line">',
            '                    <p class="text-danger remove-personalQuality">',
            '                        <span class="glyphicon glyphicon-remove"></span> Удалить',
            '                    </p>',
            '                </div>',
            '            </div>'
        ].join('');

        $(this).parent().before(html);
    });

    // удаление поля для личного качества
    $(document).on('click', 'p.remove-personalQuality', function () {
        $(this).closest('div.personalQuality').remove();
    });

    $.get("/Resume/GetRules?managerId=" + getManagerIdFromURI())
        .then(function (data) {
            var str = JSON.stringify(eval("(" + data + ")"));
            var json = JSON.parse(str);

            for (var propName in json.PersonalData) {
                $("input[name='" + propName + "']").closest('.form-group').remove();
            }
        })
        .catch(function (response) {
            console.log(response.responseText);
        });

    //// выбор даты с днями
    //$('#datePicker').datepicker({
    //    format: "dd.mm.yyyy",
    //    startView: 2,
    //    maxViewMode: 3,
    //    language: "ru",
    //    autoclose: true
    //});

    //// выбор даты только месяцы и годы
    //$(document).on('mouseover', '.datePicker-month', (function () {
    //    $(this).datepicker({
    //        format: "dd.mm.yyyy",
    //        startView: 2,
    //        minViewMode: 1,
    //        maxViewMode: 3,
    //        language: "ru",
    //        autoclose: true
    //    });
    //}));


    //// включеиня подсказок
    //$('[data-toggle="tooltip"]').tooltip();

    //// исчезновение
    //$('.disappearance').fadeOut(5000);


    //{
    //    "PesonalData" : {
    //        "Photo" : false,
    //        "DateOfBirth" : false
    //    }
    //}
});

