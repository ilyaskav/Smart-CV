$(function () {
    //добавление полей для нового учебного заведения
    $('#addInstitution').on('click', function () {
        var institutionIndex = 0;
        var institutionSection = $(this).closest('.form-group').prev().prev().find('input[type = "hidden"]').attr('name');
        if (institutionSection != undefined) {
            institutionIndex = parseInt(institutionSection.toString().charAt(institutionSection.toString().indexOf("[") + 1)) + 1;
        }

        //var hr = '';
        //if (institutionIndex > 0) hr = '<hr/>';

        var html = [
        '<div class="institution">',
        '    <input type="hidden" value="" name="Institutions[', institutionIndex, '].Id" id="Institutions_', institutionIndex, '__Id" data-val-number="The field Id must be a number." data-val="true">',
        '    <input type="hidden" value="" name="Institutions[', institutionIndex, '].ResumeId" id="Institutions_', institutionIndex, '__ResumeId" data-val-number="The field ResumeId must be a number." data-val="true">',
        '    <div class="form-group row">',
        '        <label class="col-md-2 col-form-label">Уч. заведение*</label>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Institutions[', institutionIndex, '].Name" id="Institutions_', institutionIndex, '__Name" data-val-required="Требуется поле Name." data-val="true" class="form-control text-box single-line">',
        '            <span data-valmsg-replace="true" data-valmsg-for="Institutions[', institutionIndex, '].Name" class="field-validation-valid text-danger"></span>',
        '        </div>',
        '    </div>',
        '    <div class="form-group row">',
        '        <label class="col-md-2 col-form-label">Город</label>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Institutions[', institutionIndex, '].City" id="Institutions_', institutionIndex, '__City" class="form-control text-box single-line">',
        '            <span data-valmsg-replace="true" data-valmsg-for="Institutions[', institutionIndex, '].City" class="field-validation-valid text-danger"></span>',
        '        </div>',
        '    </div>',
        '    <div class="form-group row">',
        '        <label class="col-md-2 col-form-label">Кафедра</label>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Institutions[', institutionIndex, '].Department" id="Institutions_', institutionIndex, '__Department" class="form-control text-box single-line">',
        '            <span data-valmsg-replace="true" data-valmsg-for="Institutions[', institutionIndex, '].Department" class="field-validation-valid text-danger"></span>',
        '        </div>',
        '        <div class="col-md-1 delete-line">',
        '            <p class="text-danger remove-institution">',
        '                <span class="glyphicon glyphicon-remove"></span> Удалить',
        '            </p>',
        '        </div>',
        '    </div>',
        '    <div class="form-group row">',
        '        <label class="col-md-2 col-form-label">Специальность*</label>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Institutions[', institutionIndex, '].Specialty" id="Institutions_', institutionIndex, '__Specialty" data-val-required="Требуется поле Specialty." data-val="true" class="form-control text-box single-line">',
        '            <span data-valmsg-replace="true" data-valmsg-for="Institutions[', institutionIndex, '].Specialty" class="field-validation-valid text-danger"></span>',
        '        </div>',
        '    </div>',
        '    <div class="form-group row">',
        '        <label class="col-md-2 col-form-label">Степень*</label>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Institutions[', institutionIndex, '].Degree" id="Institutions_', institutionIndex, '__Degree" data-val-required="Требуется поле Degree." data-val="true" class="form-control text-box single-line">',
        '            <span data-valmsg-replace="true" data-valmsg-for="Institutions[', institutionIndex, '].Degree" class="field-validation-valid text-danger"></span>',
        '        </div>',
        '    </div>',
        '    <div class="form-group row">',
        '        <label class="col-md-2 col-form-label">Период*</label>',
        '        <div class="col-md-9">',
        '            <div id="datePicker-month" class="input-daterange input-group datePicker-month">',
        '                <input type="datetime" value="" name="Institutions[', institutionIndex, '].From" id="Institutions_', institutionIndex, '__From" data-val-required="Требуется поле From." data-val-date="The field From must be a date." data-val="true" class="form-control text-box single-line">',
        '                <span data-valmsg-replace="true" data-valmsg-for="Institutions[', institutionIndex, '].From" class="field-validation-valid text-danger"></span>',
        '                <span class="input-group-addon">по</span>',
        '                <input type="datetime" value="" placeholder="настоящее время" name="Institutions[', institutionIndex, '].To" id="Institutions_', institutionIndex, '__To" data-val-date="The field To must be a date." data-val="true" class="form-control text-box single-line">',
        '            </div>',
        '        </div>',
        '    </div>',
        '</div>',
        '<hr/>'
        ].join('');

        $(this).parent().parent().before(html);
    });

    // удаление учебного заведения
    $(document).on('click', '.remove-institution', function () {
        var institutionIndex = $(this).closest('div.institution').find('input[type = "hidden"]').val();
        var managerId = getManagerIdFromURI();

        window.location.href = '/Resume/RemoveInstitution/?managerId=' + managerId + '&institutionId=' + institutionIndex;
    });

    // разделение учебных заведений
    var separateInstitutions = function () {
        if ($('div.institution').size() == 0) return;

        $('div.institution').after('<hr/>');
    }
    separateInstitutions();

    // выбор даты только месяцы и годы
    $(document).on('mouseover', '.datePicker-month', (function () {
        $(this).datepicker({
            format: "dd.mm.yyyy",
            startView: 2,
            minViewMode: 1,
            maxViewMode: 3,
            language: "ru",
            autoclose: true
        });
    }));

    // включеиня подсказок
    //$('[data-toggle="tooltip"]').tooltip();

});