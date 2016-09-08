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

        var id = $(this).closest('.form-group').find('div.duty');
        if (id.length == 0) id = 0;
        else {
            id=id.closest('div.workplace').find('input[type="hidden"]').val();
        }

        var html = [
'<div class="duty">',
'                <div class="col-md-8 col-md-offset-2">',
'                    <input type="hidden" value="" name="WorkPlaces[', workPlaceId, '].Duties[', dutyId, '].Id" id="WorkPlaces_', workPlaceId, '__Duties_', dutyId, '__Id" data-val-number="The field Id must be a number." data-val="true">',
'                    <input type="hidden" value="',id,'" name="WorkPlaces[', workPlaceId, '].Duties[', dutyId, '].WorkPlaceId" id="WorkPlaces_', workPlaceId, '__Duties_', dutyId, '__WorkPlaceId" data-val-required="Требуется поле WorkPlaceId." data-val-number="The field WorkPlaceId must be a number." data-val="true">',
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
        var workId = 0;
        var row = $(this).closest('div.row').prev();
        var workPlace = row.find('div.workplace').last();
        if (workPlace.length != 0) {
            var str = workPlace.find('input[type="hidden"]').attr('name').toString();
            workId = parseInt(str.charAt(str.lastIndexOf("[") + 1)) + 1;
        }


        var html = [
'        <div class="workplace">',
'           <input data-val="true" data-val-number="The field Id must be a number." id="WorkPlaces_', workId, '__Id" name="WorkPlaces[', workId, '].Id" type="hidden" value="">',
'    <div class="form-group">',
'        <label class="col-md-2 control-label">Место работы*</label>',
'        <div class="col-md-9">',
'            <input class="form-control text-box single-line" data-val="true" data-val-required="The Name field is required." id="WorkPlaces_', workId, '__Name" name="WorkPlaces[', workId, '].Name" type="text" value="">',
'            <span class="field-validation-valid text-danger" data-valmsg-for="WorkPlaces[', workId, '].Name" data-valmsg-replace="true"></span>',
'        </div>',
'    </div>',

'    <div class="form-group">',
'        <label class="col-md-2 control-label">Город</label>',
'        <div class="col-md-9">',
'            <input class="form-control text-box single-line" id="WorkPlaces_', workId, '__City" name="WorkPlaces[', workId, '].City" type="text" value="">',
'            <span class="field-validation-valid text-danger" data-valmsg-for="WorkPlaces[', workId, '].City" data-valmsg-replace="true"></span>',
'        </div>',
'    </div>',

'   <div class="form-group">',
'        <label class="col-md-2 control-label">Описание</label>',
'        <div class="col-md-9">',
'            <input class="form-control text-box single-line" id="WorkPlaces_', workId, '__Description" name="WorkPlaces[', workId, '].Description" type="text" value="">',
'            <span class="field-validation-valid text-danger" data-valmsg-for="WorkPlaces[', workId, '].Description" data-valmsg-replace="true"></span>',
'        </div>',
'    </div>',

'    <div class="form-group">',
'        <label class="col-md-2 control-label">Должность*</label>',
'        <div class="col-md-9">',
'            <input class="form-control text-box single-line" data-val="true" data-val-required="The Position field is required." id="WorkPlaces_', workId, '__Position" name="WorkPlaces[', workId, '].Position" type="text" value="">',
'            <span class="field-validation-valid text-danger" data-valmsg-for="WorkPlaces[', workId, '].Position" data-valmsg-replace="true"></span>',
'        </div>',
'   <div class="col-md-1 delete-line">',
'            <p class="text-danger remove-workplace">',
'                <span class="glyphicon glyphicon-remove"></span> Удалить',
'            </p>',
'        </div>',
'    </div>',
'    <div class="form-group">',
'        <label class="col-md-2 control-label">Обязанности*</label>',
'    <div class="duty">',
'            <div class="col-md-9">',
'                <input data-val="true" data-val-number="The field Id must be a number." id="WorkPlaces_', workId, '__Duties_0__Id" name="WorkPlaces[', workId, '].Duties[0].Id" type="hidden" value="">',
'                <input data-val="true" data-val-number="The field WorkPlaceId must be a number." data-val-required="The WorkPlaceId field is required." id="WorkPlaces_', workId, '__Duties_0__WorkPlaceId" name="WorkPlaces[', workId, '].Duties[0].WorkPlaceId" type="hidden" value="">',
'                <input class="form-control text-box single-line" data-val="true" data-val-required="The Name field is required." id="WorkPlaces_', workId, '__Duties_0__Name" name="WorkPlaces[', workId, '].Duties[0].Name" type="text" value="">',
'                <span class="field-validation-valid text-danger" data-valmsg-for="WorkPlaces[', workId, '].Duties[0].Name" data-valmsg-replace="true"></span>',
'            </div>',
'    </div>',

'            <div class="col-md-9 col-md-offset-2">',
'                <p id="addDuty" aria-label="Добавить обязанность" class="text-info">',
'                    <span class="glyphicon glyphicon-plus"></span> Добавить обязанность',
'                </p>',
'            </div>',
'       </div>',

'        <div class="form-group">',
'            <label class="col-md-2 control-label">Период*</label>',
'            <div class="col-md-9">',
'                <div class="input-daterange input-group datePicker-month" id="datePicker-month">',             
'                    <input class="form-control text-box single-line" data-val="true" data-val-date="The field From must be a date." data-val-required="The From field is required." id="WorkPlaces_', workId, '__From" name="WorkPlaces[', workId, '].From" type="datetime" value="">',
'                    <span class="input-group-addon">по</span>',                    
'                    <input class="form-control text-box single-line" data-val="true" data-val-date="The field To must be a date." id="WorkPlaces_', workId, '__To" name="WorkPlaces[', workId, '].To" placeholder="настоящее время" type="datetime" value="">',
'                    <span class="field-validation-valid text-danger" data-valmsg-for="WorkPlaces[', workId, '].From" data-valmsg-replace="true"></span>',
'                </div>',
'            </div>',
'    </div>',
'<hr/>',
'</div>'

        ].join('');

        row.append(html);
    });

    // удаление полей работы
    $(document).on('click', 'p.remove-workplace', function () {
        var workplace = $(this).closest('div.workplace');
        if (workplace.find('input[type="hidden"]').val() == "") workplace.remove();
        else {
            var managerId = getManagerIdFromURI();
            var workPlaceId = workplace.find('input[type="hidden"]').val();
            //var workPlaceId = parseInt(workName.charAt(workName.indexOf("[") + 1)) + 1;

            window.location.href = '/Resume/RemoveWork/?managerId=' + managerId + '&workplaceId=' + workPlaceId;
        }
    });

// разделение мест работы
separate('div.workplace');

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