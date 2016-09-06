$(function () {
    // добавление полей для сертификата
    $('#addSkill').on('click', function () {
        var skillId = 0;
        var row = $(this).closest('div.form-group').prev();
        var skill = row.find('div.skill').last();
        if (skill.length != 0) {
            var str = skill.find('input[type="hidden"]').attr('name').toString();
            skillId = parseInt(str.charAt(str.lastIndexOf("[") + 1)) + 1;
        }

        var html = [
        '<div class="skill">',
        '    <input type="hidden" value="" name="Skills[', skillId, '].Id" id="Skills_', skillId, '__Id" data-val-number="The field Id must be a number." data-val="true">',
        '    <div class="form-group">',
        '        <label class="col-md-2 control-label">Навык*</label>',
        '        <div class="col-md-9">',
        '            <input type="text" value="" name="Skills[', skillId, '].Name" id="Skills_', skillId, '__Name" data-val-required="Требуется поле Name." data-val="true" class="form-control text-box single-line">',
        '            <span data-valmsg-replace="true" data-valmsg-for="Skills[', skillId, '].Name" class="field-validation-valid text-danger"></span>',
        '        </div>',
        '        <div class="col-md-1 delete-line">',
        '            <p class="text-danger remove-skill">',
        '                <span class="glyphicon glyphicon-remove"></span> Удалить',
        '            </p>',
        '        </div>',
        '    </div>',
        '<hr />',
        '</div>'
        ].join('');
        
        row.append(html);
    });

    // удаление поля для навыка
    $(document).on('click', 'p.remove-skill', function () {
        var skill = $(this).closest('div.skill');
        if (skill.find('input[type="hidden"]').val() == "") {
            skill.remove();
        }
        else {
            var managerId = getManagerIdFromURI();
            var skillId = skill.find('input[type="hidden"]').val();

            window.location.href = '/Resume/RemoveSkill/?managerId=' + managerId + '&skillId=' + skillId;
        }
    });

    separate('div.skill');
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

});