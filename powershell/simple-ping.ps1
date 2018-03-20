$net = "10.0.0."
$range = 80,8080,443
$iprange = 1..254
foreach ($i in $iprange)
{
    $ip = $net + $i
    foreach ($r in $range)
    {
        Try{
            $socket = new-object System.Net.Sockets.TcpClient($ip, $r)
            If($socket.Connected)
            {
                "$ip listening to port $r"
            }
            else{}
        }
        Catch
        {
        }
        Finally{
        if ($socket -ne $null){
            $socket.Close()
            }
        }
    }
}
