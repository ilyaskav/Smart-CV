$(function () {
    //добавление дополнительного поля для контакта
    $('#addContact').on('click', function () {
        var contactIndex = 0;
        var contactSection = $(this).parent().prev().find('input[type = "hidden"]').attr('name');
        if (contactSection != undefined) {
            contactIndex = parseInt(contactSection.toString().charAt(contactSection.toString().indexOf("[") + 1)) + 1;
        }

        var html = [
            '<div class="contact">',
            '    <div class="form-group row">',
            '        <input type="hidden" value="" name="Contacts[', contactIndex, '].Id" id="Contacts_', contactIndex, '__Id" data-val-number="The field Id must be a number." data-val="true">',
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
        const contactId = $(this).closest('div.contact').find('input[type = "hidden"]').val();

        if (contactId) {
            var managerId = getManagerIdFromURI();
            window.location.href = '/Resume/DeleteContact/?managerId=' + managerId + '&contactId=' + contactId;
        }
        else {
            $(this).closest('div.contact').remove();
        }
    });

});