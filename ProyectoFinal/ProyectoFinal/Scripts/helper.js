var FormModes = {
    Add: 0,
    Edit: 1,
    Delete: 2,
    View: 3
};

function GetFormMode(formMode)
{
    switch (formMode) {
        case 0:
            return FormModes.Add;
        case 1:
            return FormModes.Edit;
        case 2:
            return FormModes.Delete;
        case 3:
            return FormModes.View;
        default:
            return 'undefined';
    }
}



var Relocator = function() {
    this.GoToAdd = function() {
        window.location = '/Clientes/Data?formMode=' + FormModes.Add;
    },
    this.GoToEdit = function(id) {
        window.location = '/Clientes/Data?id=' + id + "&formMode=" + FormModes.Edit;
    },
    this.GoToDelete = function(id) {
        window.location = '/Clientes/Data?id=' + id + "&formMode=" + FormModes.Delete;
    },
    this.GoToView = function(id) {
        window.location = '/Clientes/Data?id=' + id + "&formMode=" + FormModes.View;
    };
};

var RelocatorHelper = new Relocator();