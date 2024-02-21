package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
	"time"

	"github.com/prometheus/client_golang/prometheus"
	"github.com/prometheus/client_golang/prometheus/promauto"
	"github.com/prometheus/client_golang/prometheus/promhttp"
)

func recordMetrics() {
	messageEndpoint := os.Getenv("MSG_ENDPOINT")
	log.Printf("MSG_ENDPOINT: %v", messageEndpoint)
	if messageEndpoint == "" {
		log.Fatalf("Invalid env var MSG_ENDPOINT: %s", messageEndpoint)
	}
	go func() {
		for {
			opsProcessed.Inc()
			num = (num + 1) % 4
			someState.Set((float64(num)))

			getMessage(messageEndpoint)
			time.Sleep(2 * time.Second)
		}
	}()
}

var (
	opsProcessed = promauto.NewCounter(prometheus.CounterOpts{
		Name: "hitmanGoService_processed_ops_total",
		Help: "The total number of processed events",
	})
	someState = promauto.NewGauge(prometheus.GaugeOpts{
		Name: "hitmanGoService_some_state",
		Help: "The value of some state",
	})
	num           = 0
	metric_gauges = map[string]prometheus.Gauge{}
)

func main() {
	recordMetrics()
	fmt.Println("Starting metric server")
	http.Handle("/metrics", promhttp.Handler())
	http.ListenAndServe(":2112", nil)
}
