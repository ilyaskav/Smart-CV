//  http://htmltojavascript.com/
// https://www.sitepoint.com/16-jquery-selectboxdrop-down-plugins/

$(function () {
    //добавление дополнительного поля для контакта
    $('#addContact').on('click', function () {
        var contactIndex=0;
        var contactSection = $(this).parent().prev().find('input[type = "hidden"]').attr('name');
        //console.log(contactSection);
        if (contactSection != undefined) {
            contactIndex =parseInt( contactSection.toString().charAt(contactSection.toString().indexOf("[") + 1)) +1;
        }

        //console.log(contactIndex);

        var html = [
        '<div class="contact">',
        '    <div class="form-group">',
        '        <input type="hidden" value="" name="Contacts[',contactIndex,'].Id" id="Contacts_',contactIndex,'__Id" data-val-number="The field Id must be a number." data-val="true">',
        '        <input type="hidden" value="" name="Contacts[', contactIndex, '].ContactTitle.Id" id="Contacts_', contactIndex, '__ContactTitle_Id" data-val-required="Требуется поле Id." data-val-number="The field Id must be a number." data-val="true">',
        '        <div class="col-md-2">',
        '            <input type="text" value="" name="Contacts[', contactIndex, '].ContactTitle.Title" id="Contacts_', contactIndex, '__ContactTitle_Title" class="form-control text-right font-bold text-box single-line">',
        '        </div>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Contacts[', contactIndex, '].Data" id="Contacts_', contactIndex, '__Data" data-val-required="Требуется поле Data." data-val="true" class="form-control text-box single-line">',
        '        </div>',
        '        <div class="col-md-1 delete-line">',
        '            <p class="text-danger remove-contact">',
        '                <span class="glyphicon glyphicon-remove"></span> Удалить',
        '            </p>',
        '        </div>',
        '    </div>',
        '</div>'
        ].join('');

        $(this).parent().before(html);
    });

    // удаление поля для контакта
    $(document).on('click', 'p.remove-contact', function () {
        $(this).closest('div.contact').remove();
    });

    //добавление полей для нового учебного заведения
    $('#addInstitution').on('click', function () {
        var html = [
'    <hr/>',
'    <div class="row">',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Уч. заведение*</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="name">',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Город</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="city" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Кафедра</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="department" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Специальность*</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="speciality" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Степень*</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="degree" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Период*</label>',
'            <div class="col-md-10">',
'                <div class="input-daterange input-group" id="datePicker">',
'                    <input type="text" class="input-sm form-control" name="start" />',
'                    <span class="input-group-addon">по</span>',
'                    <input type="text" class="input-sm form-control" placeholder="настоящее время" name="end" />',
'                </div>',
'            </div>',
'        </div>',
'    </div>',
''
        ].join('');
        $(this).parent().parent().before(html);
    });

    // добавление поля для обязанности на работе
    $(document).on('click', '#addDuty', function () {
        var html = [
'            <div class="duty">',
'                <div class="col-md-9 col-md-offset-2">',
'                    <input type="text" class="form-control" name="duty" />',
'                </div>',
'                <div class="col-md-1 delete-line">',
'                    <p class="text-danger remove-duty">',
'                        <span class="glyphicon glyphicon-remove"></span> Удалить',
'                    </p>',
'                </div>',
'            </div>',
''
        ].join('');

        $(this).parent().before(html);
    });

    // удаление поля для обязанности на работе
    $(document).on('click', 'p.remove-duty',function () {
        $(this).closest('div.duty').remove();
    });

    // добавление полей для нового места работы
    $('#addWorkPlace').on('click', function () {
        var html = [
'    <hr>    ',
'    <div class="row">',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Место работы*</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="name">',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Город</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="city" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Описание</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="description" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Должность*</label>',
'            <div class="col-md-10">',
'                <input type="text" class="form-control" name="position" />',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Обязанности*</label>',
'            <div class="col-md-9">',
'                <input type="text" class="form-control" name="duty" />',
'            </div>',
'            <div class="duty">',
'                <div class="col-md-9 col-md-offset-2">',
'                    <input type="text" class="form-control" name="duty" />',
'                </div>',
'                <div class="col-md-1 delete-line">',
'                    <p class="text-danger remove-duty">',
'                        <span class="glyphicon glyphicon-remove"></span> Удалить',
'                    </p>',
'                </div>',
'            </div>',
'            <div class="col-md-10 col-md-offset-2">',
'                <p id="addDuty" aria-label="Добавить обязанность" class="text-info">',
'                    <span class="glyphicon glyphicon-plus"></span> Добавить обязанность',
'                </p>',
'            </div>',
'        </div>',
'        <div class="form-group">',
'            <label class="col-md-2 control-label">Период*</label>',
'            <div class="col-md-10">',
'                <div class="input-daterange input-group" id="datePicker">',
'                    <input type="text" class="input-sm form-control" name="start" />',
'                    <span class="input-group-addon">по</span>',
'                    <input type="text" class="input-sm form-control" placeholder="настоящее время" name="end" />',
'                </div>',
'            </div>',
'        </div>',
'    </div>',
''
        ].join('');

        $(this).parent().parent().parent().before(html);
    });

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

    // включение поля с выпадающим списком
    //$('.dropdown-contact').ddslick({
    //    onSelected: function (selectedData) {
    //        //callback function: do something with selectedData;
    //    }
    //});

    //var selectedData = function (data) {
    //    $('.dropdown-contact').val(data);
    //}

    // выбор даты с днями
    $('#datePicker').datepicker({
        format: "dd.mm.yyyy",
        startView: 2,
        maxViewMode: 3,
        language: "ru",
        autoclose: true
    });

    // выбор даты только месяцы и годы
    $('#datePicker-month').datepicker({
        format: "dd.mm.yyyy",
        startView: 2,
        minViewMode: 1,
        maxViewMode: 3,
        language: "ru",
        autoclose: true
    });

    // включеиня подсказок
    $('[data-toggle="tooltip"]').tooltip();

    // исчезновение
    $('.disappearance').fadeOut(5000);
});

