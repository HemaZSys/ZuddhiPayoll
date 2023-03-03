var i = 0;
$(document).ready(function () {
	$('#add_more').on('click', function () {
		var colorR = Math.floor((Math.random() * 256));
		var colorG = Math.floor((Math.random() * 256));
		var colorB = Math.floor((Math.random() * 256));
		i++;
		var html = '<hr><p>Reimbursements</p><div id="append_no_' + i + '" class="animated bounceInLeft">' +
			'</div class="row">' +
			'</div class="Column">' +
			'<table>' +
			'<tr>' +
			'<td>' +
			'<input type="text" placeholder="Books and Periodicals" class="form-control"/>' +
			'</td>' +
			'</tr>' +
			'</table>' +
			'</div></div>';
		$('#container').append(html);
		$('#remove_more').fadeIn(function () {
			$(this).show();
		});
	});

	$('#remove_more').on('click', function () {
		$('#append_no_' + i).removeClass('bounceInLeft').addClass('bounceOutRight')
			.fadeOut(function () {
				$(this).remove();
			});
		i--;
		if (i == 0) {
			$('#remove_more').fadeOut(function () {
				$(this).hide()
			});;
		}

	});
});