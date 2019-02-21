/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/* 
    JavaScript Name : Health Inspections Dashboard
    Created on      : December  13, 2017, 17:27:01
    Last edited on  : January   24, 2018, 19:04:43
    Author          : AL [Wessex Water]
*/


$( window ).on( 'load', function() {
    detailGroupSameHeight();
    colImageSameHeight();
    
    $( window ).on( 'resize', function() {
        detailGroupSameHeight();
        colImageSameHeight( true );
    } );
} );

var settingsDataTable   = {
    'destroy'       : true, 
    'language'      : {
        'lengthMenu'        : 'Show _MENU_ Entries',            // Length of records text
        'search'            : '',                               // Search label text
        'searchPlaceholder' : 'Search...',                      // Search input placeholder
        'paginate'          : {
            previous: "<span class='span-pagination left'><i class='fas fa-angle-left'></i></span>", 
            next    : "<span class='span-pagination right'><i class='fas fa-angle-right'></i></span>", 
            first   : "<span class='span-pagination right'><i class='fas fa-angle-double-left'></i></span>", 
            last    : "<span class='span-pagination right'><i class='fas fa-angle-double-right'></i></span>" 
        }
    }, 
    'lengthMenu'    : [ 
        [ 10, 25, 50, 75, 100, -1 ],                            // Items in length of records dropdown
        [ 10, 25, 50, 75, 100, 'All' ]                          // Label for the options in length of records dropdown
    ], 
    'order'         : [],                                       // Disable sort on load
    'paging'        : true,                                     // Enabling the pagination
    'pageLength'    : 10,                                       // Number of rows to display on a single page
    'pagingType'    : 'full_numbers',                           // Type of pagination - 'Previous' and 'Next' buttons only
    'responsive'    : false,
    'scrollX'       : true,
    'searching'     : true, 
    'dom'           : 'Blfrtip' 
    //'buttons': [
    //    {
    //        extend: 'excel',
    //        exportOptions: {
    //            columns: [0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17]
    //        }
    //    } 
    //] 
};

var settingsDataTableInspection = {
    'destroy': true,
    'language': {
        'lengthMenu': 'Show _MENU_ Entries',            // Length of records text
        'search': '',                               // Search label text
        'searchPlaceholder': 'Search...',                      // Search input placeholder
        'paginate': {
            previous: "<span class='span-pagination left'><i class='fas fa-angle-left'></i></span>",
            next: "<span class='span-pagination right'><i class='fas fa-angle-right'></i></span>",
            first: "<span class='span-pagination right'><i class='fas fa-angle-double-left'></i></span>",
            last: "<span class='span-pagination right'><i class='fas fa-angle-double-right'></i></span>"
        }
    },
    'lengthMenu': [
        [10, 25, 50, 75, 100, -1],                            // Items in length of records dropdown
        [10, 25, 50, 75, 100, 'All']                          // Label for the options in length of records dropdown
    ],
    'order': [],                                       // Disable sort on load
    'paging': true,                                     // Enabling the pagination
    'pageLength': 10,                                       // Number of rows to display on a single page
    'pagingType': 'full_numbers',                           // Type of pagination - 'Previous' and 'Next' buttons only
    'responsive': false,
    'scrollX': true,
    'searching': true,
    'dom': 'Blfrtip',
    'buttons': [
        {
            extend: 'excel',
            exportOptions: {
                columns: [0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11]
            }
        } 
    ] 
};

var settingsDataTableForInspextionExt = {
    'destroy': true,
    'language': {
        'lengthMenu': 'Show _MENU_ Entries',            // Length of records text
        'search': '',                               // Search label text
        'searchPlaceholder': 'Search...',                      // Search input placeholder
        'paginate': {
            previous: "<span class='span-pagination left'><i class='fas fa-angle-left'></i></span>",
            next: "<span class='span-pagination right'><i class='fas fa-angle-right'></i></span>",
            first: "<span class='span-pagination right'><i class='fas fa-angle-double-left'></i></span>",
            last: "<span class='span-pagination right'><i class='fas fa-angle-double-right'></i></span>"
        }
    },
    'lengthMenu': [
        [10, 25, 50, 75, 100, -1],                            // Items in length of records dropdown
        [10, 25, 50, 75, 100, 'All']                          // Label for the options in length of records dropdown
    ],
    'order': [],                                       // Disable sort on load
    'paging': true,                                     // Enabling the pagination
    'pageLength': 10,                                       // Number of rows to display on a single page
    'pagingType': 'full_numbers',                                 // Type of pagination - 'Previous' and 'Next' buttons only
    'responsive': false,
    'searching': true,
    'dom': 'Blfrtip',
    'buttons': [
        {
            extend: 'excel',
            exportOptions: {
                columns: [0, 1, 2, 3, 4, 5, 6, 7]
            }
        }
    ]
};


$( document ).ready( function() {
    
    if( $.isFunction( $.fn.DataTable ) && $( '.table-dataTable' ).length > 0 ) {
        $( '.table-dataTable' ).not( '.table-ajax' ).DataTable( settingsDataTable );
    }

    var settingsDataTableAllRecords = settingsDataTable;
    settingsDataTableAllRecords.columnDefs = [
        { searchable: false, targets: [0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16,17, 18, 19] }
    ];
    settingsDataTableAllRecords.buttons = [
        {
            extend: 'excel',
            exportOptions: {
                columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19]
            }
        }
    ];

    var tableAllRecords = $("#tableAllRecords");
    if ($.isFunction($.fn.DataTable)) {
        if ($.fn.DataTable.isDataTable($(tableAllRecords))) {
            $(tableAllRecords).DataTable().destroy();
            $(tableAllRecords).dataTable(settingsDataTableAllRecords);
        }
    }

    $( window ).on( 'scroll', function() {
        checkWindowScroll();
    } );
    
    $( window ).on( 'resize', function() {
        if( this.resizeTO ) clearTimeout( this.resizeTO );
        this.resizeTO   = setTimeout( function() {
            $( this ).trigger( 'resizeEnd' );
        }, 300 );
        
        checkScreenWidth();
    } );
    
    $( '.link-menu' ).on( 'click', function() {
        $( 'body' ).toggleClass( 'showMenu' );
    } );
    
    
    if( $.isFunction( $.fn.datepicker ) ) {
        $( '.input-datepicker' ).datepicker( {
            changeMonth : true, 
            changeYear  : true, 
            dateFormat  : 'yy-mm-dd', 
            maxDate     : '0', 
            minDate     : '-5Y', 
            orientation : 'bottom' 
        } );
        
        $( '.input-datepicker.date-from' ).datepicker().on( 'change', function() {
            $( '.input-datepicker.date-to' ).datepicker( 'option', 'minDate', getDate( this ) );
        } );
        
        $( '.input-datepicker.date-to' ).datepicker().on( 'change', function() {
            $( '.input-datepicker.date-from' ).datepicker( 'option', 'maxDate', getDate( this ) );
        } );
    }
    
    
    if( $.isFunction( $.fn.prettyPhoto ) ) {
        $( 'a[rel^="prettyPhoto"]' ).prettyPhoto( {
            deeplinking     : false, 
            social_tools    : false, 
            changepicturecallback: function () {
                $('.pp_expand').on('click', function () {
                    var imgSrc = $(this).siblings('#pp_full_res').find('img').attr('src');
                    window.open(imgSrc, '_blank');

                    $.prettyPhoto.close();
                });
                $('#pp_full_res img').on('click', function () {
                    var imgSrc = $(this).find('img').attr('src');
                    window.open(imgSrc, '_blank');

                    $.prettyPhoto.close();
                });
            }

        } );
    }
    
    $( '.image-delete' ).on( 'click', function() {
        var colImage        = $( this ).closest( '.col-image' );
        var colAttachment   = $( this ).closest( '.col-attachment' );
        
        if( colImage.length > 0 ) {
            $( colImage ).fadeOut( 'fast', function() {
                $( this ).remove();
                colImageSameHeight();
            } );
        } else if( colAttachment.length > 0 ) {
            $( colAttachment ).fadeOut( 'fast', function() {
                $( this ).remove();
            } );
            
        }
    } );
    
    
    $( '.listing-tabs' ).each( function() {
        var checkSelected   = $( this ).find( 'li.Selected' );
        var targetElement   = $( checkSelected ).find( 'a' );
        
        if( !$( checkSelected ).length > 0 ) {
            $( this ).find( 'li:first-child' ).addClass( 'Selected' );
            targetElement   = $( this ).find( 'li.Selected a' );
        }
            
        tabsControl( targetElement, true );
    } );
    $( '.listing-tabs li a' ).on( 'click', function() {
        var targetElement   = $( this );
        tabsControl( targetElement );
    } );
    
    
    $( '.panel-gototop a' ).on( 'mouseover', function() {
        $( this ).addClass( 'hover' );
    } ).on( 'mouseout', function() {
        if( !$( this ).hasClass( 'clicked' ) ) {
            $( this ).removeClass( 'hover' );
        }
    } );
    
} );


function checkScreenWidth() {
    $( 'body' ).removeClass( 'showMenu' );
}


function checkWindowScroll() {
    var posWindow   = $( window ).scrollTop();
    
    if( posWindow >= 15 ) {
        $( 'body' ).addClass( 'onScroll' );
    } else {
        $( 'body' ).removeClass( 'onScroll' );
    }
    
    if( posWindow > 100 ) {
        $( '.panel-gototop' ).fadeIn( 'slow' );
    } else {
        $( '.panel-gototop' ).fadeOut( 'slow' );
    }
}


function detailGroupSameHeight() {
    var widthScreen     = $( window ).width();
    
    if( $.isFunction( $.fn.responsiveEqualHeightGrid ) && widthScreen >= 768 ) {
        $( '.detail-group' ).responsiveEqualHeightGridDestroy();
        $( '.detail-group' ).not( '.full-width' ).responsiveEqualHeightGrid();
    } else {
        $( '.detail-group' ).height( 'auto' );
    }
    
    $( window ).on( 'scroll', function() {
        checkWindowScroll();
    } );
}


function colImageSameHeight( resized ) {
    var widthScreen     = $( window ).width();
    
    if( resized ) {
        $( '.col-image' ).height( 'auto' );
        $( '.col-image a' ).height( 'auto' );
    }
    
    if( $.isFunction( $.fn.responsiveEqualHeightGrid ) && widthScreen >= 768 ) {
        $( '.col-image' ).responsiveEqualHeightGridDestroy();
        $( '.col-image' ).responsiveEqualHeightGrid();

        $( '.col-image a' ).each( function() {
            $( this ).height( $( this ).parent( '.col-image' ).height() - 22 );
        } );
    } else {
        $( '.col-image' ).height( 'auto' );
        $( '.col-image a' ).height( 'auto' );
    }
    
    $( window ).on( 'scroll', function() {
        checkWindowScroll();
    } );
}


function doneType() {
    $( '.map-overlay' ).show();
    var googleAPIKey    = "AIzaSyB5dQdbOhlTDKDxdr1zbsjtfoNhmM5dUPs";
    var gpsInputVal     = $( '#gps-coord' ).val();
        gpsInputVal     = encodeURIComponent( gpsInputVal );
    
    if( gpsInputVal.length > 0 ) {
        var iframeMap       = $( '#map-gpscoord iframe' );
        var newMapHREF      = "https://www.google.com/maps/embed/v1/place?q=" + gpsInputVal + "&key="+ googleAPIKey;

        $( iframeMap ).attr( 'src', newMapHREF );
        $( iframeMap ).src  = newMapHREF;
    
        setTimeout( function() {
            $( '.map-overlay' ).hide();
        }, 3000 );
    } else {
        $( '.map-overlay' ).hide();
    }
}


function getDate( element ) {
    var date;
    try {
        date    = $.datepicker.parseDate( 'yy-mm-dd', element.value );
    } catch( error ) {
        date    = null;
    }
    
    return date;
}


/**
 * @name        tabsControl
 * @param       {element} targetElement The element which the function being clicked
 * @param       {boolean} init          To trigger if upon initialization or click
 * @description 
 *              Tabbing control functions - To show and hide tab content
 */
function tabsControl( targetElement, init ) {
    var tabTarget           = $( targetElement ).attr( 'data-tabtarget' );
    var tabTargetContent    = $( '#tab-' + tabTarget );
    
    if( init ) {
        $( tabTargetContent ).addClass( 'Active' );
    } else {
        $( targetElement ).blur();
        
        var tabLi           = $( targetElement ).closest( 'li' );
        
        if( !$( tabLi ).hasClass( 'Selected' ) || !$( tabTargetContent ).hasClass( 'Active' ) ) {
            var tabsContentActive   = $( '.section-tabs' ).find( '.layer-tabcontent.Active' );
            
            $( tabLi ).siblings( 'li' ).removeClass( 'Selected' );
            $( tabLi ).addClass( 'Selected' );
            
            $( tabsContentActive ).fadeOut( 300, function() {
                $( tabsContentActive ).removeClass( 'Active' );
                $( tabsContentActive ).removeAttr( 'style' );
                
                $( tabTargetContent ).fadeIn( 300, function() {
                    $( tabTargetContent ).addClass( 'Active' );
                    $( window ).trigger( 'resize' );
                } );
            } );
        }
    }
}


/**
 * @name        goToTop
 * @function
 * @param       {element} el The element which the function being clicked
 * @description 
 *              Function to scroll the page to top. Triggered by onClick.
 */
function goToTop( el ) {
    $( el ).addClass( 'hover clicked' );
    
    $( 'html, body' ).animate( {
        scrollTop   : 0 
    }, 700, function() {
        $( el ).removeClass( 'hover clicked' );
    } );
}
