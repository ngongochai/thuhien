(function($){var $textTelControls=$('.editable-control[data-type="text"],.editable-control[data-type="tel"]');$.fn.editableForm=function(options){if(options=="item"){return $(this).next('.generated-field');}else if(options=="update"){$.fn.editableForm.updateFormContent(this);}else if(options=="saveButton"){return $(this).find('button.fe-btn-save');}else if(options=="cancelButton"){return $(this).find('button.fe-btn-cancel');}
else{var settings=$.extend({button_cancel_label:"Cancel",button_save_label:"Save",cancel_button_reset_form:true,item_width:"100%",item_width_sm:"100%",item_width_xs:"100%"},options);var responsiveItemWidth;function calculateItemWidth(){var body_width=$('body').width();if(body_width<992){responsiveItemWidth=settings.item_width_sm;}else if(body_width<768){responsiveItemWidth=settings.item_width_xs;}else{responsiveItemWidth=settings.item_width;}}
calculateItemWidth();var textInputTemplate='<input hidden type="text" class="generated-field form-control input-sm" style="width:'+responsiveItemWidth+'" />',selectTemplate='<select hidden class="generated-field" style="width:'+responsiveItemWidth+'"> </select>',telTemplate='<input hidden type="tel" class="generated-field form-control input-sm" style="width:'+responsiveItemWidth+'" />',editFormButtonTemplate='<span class="form-btn-edit"><button type="button" class="btn btn-sm btn-default">\
								<i class="fa fa-pencil"></i></button></span>',action_button_template='<div hidden class="editable-actions generated-field box-sm">\
							<button type="button" class="btn btn-sm btn-default fe-btn-cancel">'+settings.button_cancel_label+'</button>\
							<button type="button" class="btn btn-sm btn-primary fe-btn-save">'+settings.button_save_label+'</button>\
						</div>',$self=$(this),$childControls=$self.find('[data-type="text"],[data-type="select"],[data-type="tel"],[data-type="file"]');$self.find('.editable-control').filter(function(){if($(this).text()==""){return true;}}).each(function(){$(this).html('<em class="text-gray-light">'+$(this).data('placeholder')+'</em>');});$self.addClass('form-wrapper relative').append(editFormButtonTemplate);$(window).resize(function(){$('input.generated-field ,select.generated-field').width(responsiveItemWidth);body_with=$('body').outerWidth();if(body_with<992){responsiveItemWidth=settings.item_width_sm;}else if(body_with<768){responsiveItemWidth=settings.item_width_xs;}else{responsiveItemWidth=settings.item_width;}});function turn_this_to_input(element,type){var input_type_cases={'text':function(){return element.after(textInputTemplate).next().attr('name',element.attr('id')).attr('placeholder',element.data('placeholder'));},'select':function(){var $option_list=$('<select></select>');$.each(element.data('options'),function(index){var created_option=($('<option></option>').val(element.data('options')[index].value).text(element.data('options')[index].text));$option_list.append(created_option)});$option_list.find('option').filter(function(){if($(this).val()==element.data('selected')){return true;}}).attr('selected','selected');return element.after(selectTemplate).next().append($option_list.html()).attr('name',element.attr('id'));},'tel':function(){element.after(telTemplate).next().attr('name',element.attr('id')).attr('placeholder',element.data('placeholder'));},'file':function(){element.parents('.form-group').find('.edit-file-upload').hide().removeClass('hidden')}};if(!element.parents('form').hasClass('hasActions')){element.parents('form').addClass('hasActions').append(action_button_template);}
return input_type_cases[type]();}
$childControls.each(function(){turn_this_to_input($(this),$(this).data('type'));});$(this).on('click',function(e){if(!$(this).hasClass('form-edit')){$(this).find('.editable-control').filter(function(){if($(this).text()==$(this).data('placeholder')){return true;}}).html('');e.preventDefault();$(this).find('.editable-control').hide();$('.generated-field').each(function(){$(this).fadeIn().val($(this).prev('.editable-control').text());});$('select.generated-field').each(function(){$(this).fadeIn();});$('.editable-actions').fadeIn();$(this).addClass('form-edit');}});$(document).on('click','button.fe-btn-cancel',function(){if(settings.cancel_button_reset_form){$.fn.editableForm.cancelEditForm(this);$self.removeClass('form-edit');}
$self.find('.editable-control').filter(function(){if($(this).text()===""){return true;}}).each(function(){$(this).html('<em class="text-gray-light">'+$(this).data('placeholder')+'</em>');});$self.find('input:file').val("");$self.find('.filename').text($self.find('.filename').data('current-value'));});}};$('input:file').change(function(){var thisValue=$(this).val();if(thisValue===""){thisValue=$(this).parents('.edit-file-upload').find('.filename').data('current-value');}
var thisFileNameArray=thisValue.split('\\');var thisFileNameIndex=thisFileNameArray.length-1;var thisFileName=thisFileNameArray[thisFileNameIndex];$(this).parents('.file-upload').find('.filename').text(thisFileName);});$.fn.editableForm.updateFormContent=function(element){$textTelControls.each(function(){$(this).text($(this).next().val()).fadeIn();});$('.editable-control[data-type="select"]').each(function(){$(this).text($(this).next().find('option:selected').text()).fadeIn();});$('.generated-field').hide();$(element).removeClass('form-edit');$textTelControls.each(function(){if($(this).text()===""){$(this).html('<em class="text-gray-light">'+$(this).data('placeholder')+'</em>');}});$(element).find('.editable-control[data-type="file"]').each(function(){var $self=$(this);var $fileName=$self.next('.edit-file-upload').find('.filename');$self.find('.cv-label').text($fileName.text()).end().fadeIn();$fileName.data('current-value',$fileName.text());});$(element).find('.date-modified').removeClass('hidden');var d=new Date();var txtNow=d.getDate()+"/"+(d.getMonth()+1)+"/"+d.getFullYear();$(element).find('.upload-cv-label .date').text(txtNow);$(element).find('.error-message').fadeOut();$(element).find('.has-error').removeClass('has-error');};$.fn.editableForm.cancelEditForm=function(cancelBtn){$('.editable-control').fadeIn();$('.generated-field').hide();$('[data-type="select"]').each(function(){var currentValue=$(this).text();var idValue=$(this).next().find('option').filter(function(){if($(this).text()===currentValue){return true}}).attr('value');$(this).next().val(idValue);});var $element=$(cancelBtn).parents('.editable-form');$element.find('.error-message').fadeOut();$element.find('.has-error').removeClass('has-error');}})(jQuery);