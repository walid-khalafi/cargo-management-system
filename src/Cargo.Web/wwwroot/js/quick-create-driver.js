// Quick Create Driver jQuery AJAX Implementation
$(document).ready(function() {
    
    // Function to open the QuickCreate modal
    window.openDriverQuickCreateModal = function(companyId, companyName) {
        // Set the company ID and name in the modal
        $('#modalCompanyId').val(companyId);
        $('#modalCompanyName').text(companyName);
        
        // Clear any previous form data
        $('#quickCreateDriverForm')[0].reset();
        
        // Clear validation messages
        $('.text-danger').text('');
        
        // Show the modal
        $('#driverQuickCreateModal').modal('show');
    };
    
    // Function to submit QuickCreate form via AJAX
    window.submitQuickCreateAjax = function() {
        const form = $('#quickCreateDriverForm');
        const submitButton = form.find('button[type="submit"]');
        const modal = $('#driverQuickCreateModal');
        
        // Disable submit button and show loading state
        submitButton.prop('disabled', true)
                   .html('<i class="fas fa-spinner fa-spin"></i> Adding...');
        
        // Clear previous validation messages
        $('.text-danger').text('');
        
        $.ajax({
            url: '/Admin/Driver/QuickCreate',
            type: 'POST',
            data: form.serialize(),
            dataType: 'json',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            },
            success: function(response) {
                if (response.success) {
                    // Show success message
                    showSuccessMessage(response.message);
                    
                    // Close modal
                    modal.modal('hide');
                    
                    // Reset form
                    form[0].reset();
                    
                    // Trigger custom event for page refresh
                    $(document).trigger('driver:created', [response.data]);
                    
                    // Reload page after short delay
                    setTimeout(function() {
                        location.reload();
                    }, 1000);
                } else {
                    // Handle validation errors
                    if (response.errors) {
                        $.each(response.errors, function(key, message) {
                            const errorElement = $('[data-valmsg-for="' + key + '"]');
                            if (errorElement.length) {
                                errorElement.text(message);
                            } else {
                                // Fallback: show error in alert
                                showErrorMessage(message);
                            }
                        });
                    } else if (response.message) {
                        showErrorMessage(response.message);
                    }
                }
            },
            error: function(xhr, status, error) {
                console.error('AJAX Error:', error);
                let errorMessage = 'An error occurred while adding the driver.';
                
                try {
                    const response = JSON.parse(xhr.responseText);
                    errorMessage = response.message || errorMessage;
                } catch (e) {
                    // Keep default error message
                }
                
                showErrorMessage(errorMessage);
            },
            complete: function() {
                // Re-enable submit button
                submitButton.prop('disabled', false)
                           .html('<i class="fas fa-user-plus"></i> Add Driver');
            }
        });
    };
    
    // Helper function to show success messages
    function showSuccessMessage(message) {
        const alertDiv = $('<div>')
            .addClass('alert alert-success alert-dismissible fade show position-fixed')
            .css({
                'top': '20px',
                'right': '20px',
                'z-index': '9999',
                'min-width': '300px'
            })
            .html(`
                <i class="fas fa-check-circle"></i> ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            `);
        
        $('body').append(alertDiv);
        
        setTimeout(function() {
            alertDiv.alert('close');
        }, 5000);
    }
    
    // Helper function to show error messages
    function showErrorMessage(message) {
        const alertDiv = $('<div>')
            .addClass('alert alert-danger alert-dismissible fade show position-fixed')
            .css({
                'top': '20px',
                'right': '20px',
                'z-index': '9999',
                'min-width': '300px'
            })
            .html(`
                <i class="fas fa-exclamation-triangle"></i> ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            `);
        
        $('body').append(alertDiv);
        
        setTimeout(function() {
            alertDiv.alert('close');
        }, 5000);
    }
    
    // Auto-dismiss alerts after 5 seconds
    setTimeout(function() {
        $('.alert').alert('close');
    }, 5000);
    
    // Handle form submission
    $(document).on('submit', '#quickCreateDriverForm', function(e) {
        e.preventDefault();
        submitQuickCreateAjax();
    });
});
