var profileSuggestion = [];

profileSuggestion.tooltipTemplate = '<div class="tooltip strong-tooltip" role="tooltip"><div class="tooltip-arrow"></div><div' +
    ' class="tooltip-inner"></div></div>';

profileSuggestion.goTo = function ($el) {
    if (typeof $el.offset() !== "undefined") {
        $('body,html').animate({
            scrollTop: $el.offset().top - 100
        }, 300)
    }
};

profileSuggestion.setup = function () {
    $(document).on('click', '.fa-times-circle', function () {
        $(this).closest('.tooltip').fadeOut('fast');
    })
};

profileSuggestion.complete = function () {
    var $self = $('.take-to-next-level');
    profileSuggestion.goTo($self);
    $self.tooltip('destroy').tooltip({
        'placement': function () {
            return $(document).width() < 768 ? "top" : "left"
        }(),
        'container': 'body',
        'template': profileSuggestion.tooltipTemplate,
        'html': true,
        'title': function () {
            return $self.data('complete-tip')
        }(),
        'trigger': 'manual'
    }).tooltip('show');
};

profileSuggestion.destroyComplete = function () {
    var $self = $('.take-to-next-level');
    $self.tooltip('destroy');
};

profileSuggestion.searchable = function () {
    var $self = $('.searchable-setting .strong-tooltip');
    profileSuggestion.goTo($self);
    $self.tooltip('destroy').tooltip({
        'placement': function () {
            return $(document).width() < 768 ? "top" : "left"
        }(),
        'container': 'body',
        'template': profileSuggestion.tooltipTemplate,
        'html': true,
        'title': function () {
            return $self.data('searchable-tip')
        }(),
        'trigger': 'manual'
    }).tooltip('show');
};

profileSuggestion.destroySearchable = function () {
    var $self = $('.searchable-setting .strong-tooltip');
    $self.tooltip('destroy');
};

profileSuggestion.upgrade = function () {
    var $self = $('.take-to-next-level');
    profileSuggestion.goTo($self);
    $self.tooltip('destroy').tooltip({
        'placement': function () {
            return $(document).width() < 768 ? "top" : "left"
        }(),
        'container': 'body',
        'template': profileSuggestion.tooltipTemplate,
        'html': true,
        'title': function () {
            return $self.data('upgrade-tip')
        }(),
        'trigger': 'manual'
    }).tooltip('show');
};

profileSuggestion.destroyUpgrade = function () {
    var $self = $('.take-to-next-level');
    $self.tooltip('destroy');
};


profileSuggestion.update = function () {
    var $self = $('.current-position');
    profileSuggestion.goTo($self);
    $self.tooltip('destroy').tooltip({
        'placement': function () {
            return $(document).width() < 768 ? "top" : "right"
        }(),
        'html': true,
        'container': 'body',
        'template': profileSuggestion.tooltipTemplate,
        'title': function () {
            return $self.data('update-tip')
        }(),
        'trigger': 'manual'
    }).tooltip('show');
};

profileSuggestion.destroyUpdate = function () {
    var $self = $('.current-position');
    $self.tooltip('destroy');
};

profileSuggestion.setup();