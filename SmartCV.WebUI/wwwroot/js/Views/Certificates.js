$(function () {
    // добавление полей для сертификата
    $('#addCertificate').on('click', function () {
        var certId = 0;
        var form = $(this).closest('form');
        var certificate = form.find('div.certificate').last();
        if (certificate.length !== 0) {
            var str = certificate.find('input[type="hidden"]').attr('name').toString();
            certId = parseInt(str.charAt(str.lastIndexOf('[') + 1)) + 1;
        }

        var html = [
            '<div class="certificate gray-bottom-line">',
            '    <input type="hidden" value="" name="Certificates[', certId, '].Id" id="Certificates_', certId, '__Id" data-val-number="The field Id must be a number." data-val="true">',
            '    <div class="form-group row">',
            '        <label class="col-md-2 col-form-label">Название*</label>',
            '        <div class="col-md-9">',
            '            <input type="text" value="" name="Certificates[', certId, '].Name" id="Certificates_', certId, '__Name" data-val-required="Требуется поле Name." data-val="true" class="form-control text-box single-line valid">',
            '            <span data-valmsg-replace="true" data-valmsg-for="Certificates[', certId, '].Name" class="text-danger field-validation-valid"></span>',
            '        </div>',
            '    </div>',
            '    <div class="form-group row">',
            '        <label class="col-md-2 col-form-label">Город</label>',
            '        <div class="col-md-9">',
            '            <input type="text" value="" name="Certificates[', certId, '].Location" id="Certificates_', certId, '__Location" class="form-control text-box single-line">',
            '            <span data-valmsg-replace="true" data-valmsg-for="Certificates[', certId, '].Location" class="field-validation-valid text-danger"></span>',
            '        </div>',
            '        <div class="col-md-1 delete-line">',
            '            <p class="text-danger remove-certificate">',
            '                <span class="glyphicon glyphicon-remove"></span> Удалить',
            '            </p>',
            '        </div>',
            '    </div>',
            '    <div class="form-group row">',
            '        <label class="col-md-2 col-form-label">Дата окончания*</label>',
            '        <div class="col-md-9">',
            '            <input type="datetime" value="" name="Certificates[', certId, '].Date" id="datePicker" data-val-required="Требуется поле Date." data-val-date="The field Date must be a date." data-val="true" data-provide="datepicker" class="form-control text-box single-line datePicker-month">',
            '            <span data-valmsg-replace="true" data-valmsg-for="Certificates[', certId, '].Date" class="field-validation-valid text-danger"></span>',
            '        </div>',
            '    </div>',
            '</div>'

        ].join('');

        $(this).parent().parent().before(html);
    });

    // удаление полей для сертификата 
    $(document).on('click', 'p.remove-certificate', function () {
        var certificate = $(this).closest('div.certificate');
        if (certificate.find('input[type="hidden"]').val() == '') {
            certificate.remove();
        }
        else {
            var managerId = getManagerIdFromURI();
            var certId = certificate.find('input[type="hidden"]').val();

            window.location.href = '/Resume/RemoveCertificate/?managerId=' + managerId + '&certificateId=' + certId;
        }
    });

    // выбор даты только месяцы и годы
    $(document).on('mouseover', '.datePicker-month', function () {
        $(this).datepicker({
            format: 'dd.mm.yyyy',
            orientation: 'bottom auto',
            startView: 2,
            minViewMode: 1,
            maxViewMode: 3,
            language: 'ru',
            autoclose: true
        });
    });
});