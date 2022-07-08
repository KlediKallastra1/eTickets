function getSliderSettings() {
    return {
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 1
    }
}

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
            $('.products-slick').slick('unslick'); /* ONLY remove the classes and handlers added on initialize */
            $(divId).html('');
            $(divId).html(result); /* Remove current slides elements, in case that you want to show new slides. */
            $('.products-slick').slick(getSliderSettings()); /* Initialize the slick again */
        },
        error: function (error) {
            alert('Fail');
        }
    });
}

function postStoreFilter(cat) {
    if (document.getElementById('category-' + cat).checked) {
        $.ajax({
            type: "GET",
            data: "{'movieCategory':" + "'" + cat + "'}",
            datatype: 'html',
            url: "/Movies/FilterStore?movieCategory=" + cat,
            success: function (result) {
                $('#storeFilter').html('');
                $('#storeFilter').html(result);
            },
            error: function (error) {
                alert('Fail');
            }
        });
    }
    else {
        $.ajax({
            type: "GET",
            data: "{'movieCategory':}",
            datatype: 'html',
            url: "/Movies/FilterStore?movieCategory=",
            success: function (result) {
                $('#storeFilter').html('');
                $('#storeFilter').html(result);
            },
            error: function (error) {
                alert('Fail');
            }
        });
    }
}