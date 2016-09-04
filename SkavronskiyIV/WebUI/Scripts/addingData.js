// Полезные ссылки
//  http://htmltojavascript.com/
// https://www.sitepoint.com/16-jquery-selectboxdrop-down-plugins/

// НАЧАЛО Общие для всех функции
var getManagerIdFromURI = function () {
    var sPageURL, managerId=0;
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
// КОНЕЦ

$(function () {

    // удаление поля для навыка
    $(document).on('click', 'p.remove-skill', function () {
        $(this).closest('div.skill').remove();
    });

    // добавление полей для языка
    $('#addLanguage').on('click', function () {
        var html = [
'            <div class="language">',
'                <div class="col-md-2">',
'                    <input type="text" placeholder="Язык" class="form-control" name="language" />',
'                </div>',
'                <div class="col-md-9">',
'                    <input type="text" placeholder="Уровень владения" class="form-control" name="level" />',
'                </div>',
'                <div class="col-md-1 delete-line">',
'                    <p class="text-danger remove-language">',
'                        <span class="glyphicon glyphicon-remove"></span> Удалить',
'                    </p>',
'                </div>',
'            </div>'
        ].join('');

        $(this).parent().before(html);
    });

    // удаление полей для языка
    $(document).on('click', 'p.remove-language', function () {
        $(this).closest('div.language').remove();
    });

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

    // добавление полей для сертификата
    $('#addCertificate').on('click', function () {
        var html = [
'        <hr />',
'        <div class="certificate">',
'            <div class="form-group">',
'                <label class="col-md-2 control-label">Название*</label>',
'                <div class="col-md-10">',
'                    <input type="text" class="form-control" name="name">',
'                </div>',
'            </div>',
'            <div class="form-group">',
'                <label class="col-md-2 control-label">Город</label>',
'                <div class="col-md-10">',
'                    <input type="text" class="form-control" name="city" />',
'                </div>',
'            </div>',
'            <div class="form-group">',
'                <label class="col-md-2 control-label">Дата окончания*</label>',
'                <div class="col-md-10">',
'                    <input id="datePicker-month" data-provide="datepicker" class="form-control" name="date" />',
'                </div>',
'            </div>',
'        </div>'
        ].join('');

        $(this).parent().before(html);
    });

    // удаление полей для сертификата 
    $(document).on('click', 'p.remove-certificate', function () {
        $(this).closest('div.certificate').remove();
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
});

