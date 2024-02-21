package main

import (
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net/http"

	"github.com/prometheus/client_golang/prometheus"
	"github.com/prometheus/client_golang/prometheus/promauto"
)

func getMessage(msgEndpoint string) {
	res, err := http.Get(msgEndpoint)
	if err != nil {
		log.Println(err)
		return
	}
	if res.StatusCode == 404 || res.StatusCode >= 500 {
		return
	}
	body, err := io.ReadAll(res.Body)
	if err != nil {
		log.Println(err)
		return
	}
	//Convert the body to type string
	sb := string(body)
	var m map[string]interface{}
	err = json.Unmarshal(body, &m)
	if err != nil {
		log.Println(err)
		return
	}
	log.Printf("Got message: %s", sb)
	processGauges(m)
}

func processGauges(m map[string]interface{}) {
	gauges, ok := m["gauges"].(map[string]interface{})
	if ok {
		for k, v := range gauges {
			metric, ok := metric_gauges[k]
			if !ok {
				// If the gauge do not exists, create it
				metric = promauto.NewGauge(prometheus.GaugeOpts{
					Name: fmt.Sprintf("hitmanGoService_gauge_%s", k),
					Help: fmt.Sprintf("The value of gauge %s", k),
				})
				metric_gauges[k] = metric
			}
			value, ok := v.(float64)
			if ok {
				metric.Set(value)
			}
			log.Println("Update Gauge:", k, ", value:", v)
		}
	}
}
