<?php

class ScoreboardEntry {
	public string $name;
	public int $timestamp;
}

function die_with_json_error(string $error, int $status_code = 500) {
	class ErrorResponse {
		public string $error;
	}

	http_response_code($status_code);

	$err_obj = new ErrorResponse();
	$err_obj->error = $error;

	echo(json_encode($err_obj));
	exit();
}



header("Content-Type: application/json");

$mysqli = null;

try {
	$mysqli = new mysqli(getenv("DB_HOST"), getenv("DB_USER"), getenv("DB_PASSWORD"), getenv("DB_DATABASE"));
	// TODO: Database migrations
} catch (Exception $ex) {
	die_with_json_error("SQL error");
}

if ($_SERVER['REQUEST_METHOD'] === 'GET') {
	// TODO: Database function to retrieve best scoreboard entries
	die_with_json_error("Unimplemented");
} else if ($_SERVER['REQUEST_METHOD'] === 'POST') {
	// TODO: Database function to store a scoreboard entry
	die_with_json_error("Unimplemented");
} else {
	die_with_json_error("Unsupported request method");
}

die_with_json_error("An unknown error happened");
?>
