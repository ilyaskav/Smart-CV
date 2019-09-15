// выбор даты с днями
$('#dateOfBirth').datepicker({
    format: "dd.mm.yyyy",
    orientation: "bottom auto",
    startView: 2,
    maxViewMode: 3,
    language: "ru",
    autoclose: true
});

// включение подсказок
$('[data-toggle="tooltip"]').tooltip();
