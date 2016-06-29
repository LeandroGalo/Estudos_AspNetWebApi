var token = "";

$("#btn_login").click(function(){
	var username = $("#text_username").val();
	var password = $("#text_password").val();
	$.ajax({
		type: "POST",
		url: "http://localhost:55090/token", //aqui deve ser o endereco do backend
		data: {
			"grant_type": "password",
			"username": username,
			"password": password,
		},
		success: function(data) {
			token = data.access_token;
			console.log(data); 
		},
		error: function(data) { 
			console.log(data);
		}
	});
});

$("#btn_get").click(function(){
	$.ajax({
		type: "GET",
		url: "http://localhost:55090/api/teste", //aqui deve ser o endereco do backend
		headers: {
			"Authorization": "Bearer " + token
		},
		success: function(data) 
		{ 
			console.log(data); 
		},
		error: function(data)
		{
			console.log($.parseJSON(data.responseText).Message);
		}
	});
});