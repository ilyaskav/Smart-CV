$(function () {

    // добавление поля для обязанности на работе
    $(document).on('click', '#addDuty', function () {
        var dutyId = 0, workPlaceId=0;
        var idName = $(this).parent().prev().find('input[type = "hidden"]').attr('name');
        //var workPlaceName = $(this).parent().prev().find('input[type = "hidden"]').next().attr('name');
        if (idName != undefined) {
            dutyId = parseInt(idName.toString().charAt(idName.toString().lastIndexOf("[") + 1)) + 1;
        }
        workPlaceId = parseInt(idName.toString().charAt(idName.toString().indexOf("[") + 1));

        var html = [
'<div class="duty">',
'                <div class="col-md-9 col-md-offset-2">',
'                    <input type="hidden" value="" name="WorkPlaces[', workPlaceId, '].Duties[', dutyId, '].Id" id="WorkPlaces_', workPlaceId, '__Duties_', dutyId, '__Id" data-val-number="The field Id must be a number." data-val="true">',
'                    <input type="hidden" value="" name="WorkPlaces[', workPlaceId, '].Duties[', dutyId, '].WorkPlaceId" id="WorkPlaces_', workPlaceId, '__Duties_', dutyId, '__WorkPlaceId" data-val-required="Требуется поле WorkPlaceId." data-val-number="The field WorkPlaceId must be a number." data-val="true">',
'                    <input type="text" value="" name="WorkPlaces[', workPlaceId, '].Duties[', dutyId, '].Name" id="WorkPlaces_', workPlaceId, '__Duties_', dutyId, '__Name" data-val-required="Требуется поле Name." data-val="true" class="form-control text-box single-line">',
'                    <span data-valmsg-replace="true" data-valmsg-for="WorkPlaces[', workPlaceId, '].Duties[', dutyId, '].Name" class="field-validation-valid text-danger"></span>',
'                </div>',
'                <div class="col-md-1 delete-line">',
'                    <p class="text-danger remove-duty">',
'                        <span class="glyphicon glyphicon-remove"></span> Удалить',
'                    </p>',
'            </div>',
'    </div>'
        ].join('');

        $(this).parent().before(html);
    });

    // удаление поля для обязанности на работе
    $(document).on('click', 'p.remove-duty', function () {
        var duty = $(this).closest('div.duty');
        if (duty.find('input[type="hidden"]').val() == "") duty.remove();
        else {
            var managerId = getManagerIdFromURI();
            var workName = duty.find('input[type="hidden"]').attr('name').toString();
            //var workPlaceId = parseInt(workName.charAt(workName.indexOf("[") + 1)) + 1;
            var dutyId = parseInt(workName.charAt(workName.lastIndexOf("[") + 1)) + 1;

            window.location.href = '/Resume/RemoveDuty/?managerId=' + managerId + '&dutyId=' + dutyId;
        }
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



// разделение мест работы
var separateWorkPlaces = function () {
    if ($('div.workplace').size() == 0) return;

    $('div.workplace').after('<hr/>');
}
separateWorkPlaces();

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

});