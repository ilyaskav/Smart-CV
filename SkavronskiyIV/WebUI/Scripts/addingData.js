// http://htmltojavascript.com/

$(function () {
    //добавление дополнительного поля для контакта
    $('#addContact').on('click', function () {
        $(this).parent().before('<div class="form-group"> <div class="col-md-2"> <input type="text" class="form-control font-bold text-right" /></div>' +
            '<div class="col-md-10"> <input type="text" class="form-control" /></div></div>');
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

});

