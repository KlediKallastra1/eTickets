function postFilter(cat, index) {
    var divId;
    if (index === '0') divId = '#movieSelection1';
    else if (index === '1') divId = '#movieSelection2';
    else divId = '#movieSelection3';
    
    $.ajax({
        type: "GET",
        data: "{'movieCategory':" + "'" + cat + "'}",
        datatype: 'html',
        url: "/Movies/FilterSelection?movieCategory=" + cat,
        success: function (result) {
            console.log(index, divId);
            $(divId).html('');
            $(divId).html(result);
        },
        error: function (error) {
            alert('Fail');
        }
    });
}