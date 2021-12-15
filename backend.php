<?php
class  HighScores {
    const SERVERNAME = "127.0.0.1";
    const USERNAME = "root";
    const PASS = "root";
    const DBNAME = "HIGHSCORES";
    private $conn = null;
    
    function __construct() {}
    
    function initConnection() {
        $this->conn = new mysqli(self::SERVERNAME, self::USERNAME, self::PASS, self::DBNAME);
        // Check connection
        if ($this->conn->connect_error) {
            die("Connection failed: " . $this->conn->connect_error);
        }
    }
    
    function queryHighScores() {
        if($this->conn) {
            $reply = array("scores" => array());
            if (($result=$this->conn->query("SELECT * FROM HIGHSCORES ORDER BY score DESC"))) {
                while(($row=$result->fetch_assoc())) {
                    $reply["scores"][] = $row;
                }
            $result->close();
            } else {
                $reply["stauts"] = 'ERROR';
            }
            return $reply;
        }
        return null;
    }
    
    function insertHighScore($playername, $score) {
        if($this->conn) {
            $pn = filter_var($playername, FILTER_SANITIZE_STRING);
            $sc = filter_var($score, FILTER_VALIDATE_INT);
            if($pn and $sc) {
                $sql = 'INSERT INTO HIGHSCORES (playername, score) VALUES ("'.$pn.'", '.$sc.')';
                if($this->conn->query($sql) == TRUE) {
                    return 'OK';
                } else {
                    return $conn->error;
                }
            } else {
                return 'Parameter can not be empty';
            }
        } else {
            return 'Database connection error';
        }
    }
    
    function closeConnection() {
        if($this->conn) {
            $this->conn->close();
        }
    }
}

$hs = new HighScores();
$hs->initConnection();
header('Content-Type: application/json');
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $high_scores = $hs->queryHighScores();
    echo json_encode($high_scores);
} else if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $data = file_get_contents('php://input');
    $data_decoded = urldecode($data);
    $hsItem = json_decode($data_decoded, true);
    $ret = $hs->insertHighScore ($hsItem['playername'], $hsItem['score']);
    $response["status"] = $ret;
    $response["dbg"] = "POST received:".$hsItem['playername'].': '.$hsItem['score'];
}
$hs->closeConnection();
?>
