{{/* vim: set filetype=mustache: */}}
{{/*
Expand the name of the chart.
*/}}
{{- define "Turquoise.sync.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "Turquoise.sync.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "Turquoise.sync.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Common labels
*/}}
{{- define "Turquoise.sync.labels" -}}
helm.sh/chart: {{ include "Turquoise.sync.chart" . }}
{{ include "Turquoise.sync.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end -}}

{{/*
Selector labels
*/}}
{{- define "Turquoise.sync.selectorLabels" -}}
app: {{ include "Turquoise.sync.name" . }}
version: {{ .Chart.AppVersion  | quote }}
app.kubernetes.io/name: {{ include "Turquoise.sync.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
branch:  {{ .Values.branch }}
{{- end -}}


{{- define "Turquoise.sync.annotations" -}}
azure-pipelines/run: {{ .Values.azurepipelines.run }}
azure-pipelines/pipeline: {{ .Values.azurepipelines.pipeline }}
azure-pipelines/pipelineId: {{ .Values.azurepipelines.pipelineId }}
azure-pipelines/jobName: {{ .Values.azurepipelines.jobName }}
azure-pipelines/runuri: {{ .Values.azurepipelines.runuri | replace " " "%20" | replace "(" "%28" | replace ")" "%29" | replace "*" "%2A"}}
azure-pipelines/project: {{ .Values.azurepipelines.project | replace " " "%20" | replace "(" "%28" | replace ")" "%29" | replace "*" "%2A"}}
azure-pipelines/org: {{ .Values.azurepipelines.org }}
{{- end -}}

{{- define "Turquoise.sync.service.annotations" -}}
healthcheck/isalive: "/healthcheck/isalive"
healthcheck/isaliveandwell: "/healthcheck/isaliveandwell"
healthcheck/crontab: "*/15 * * * *"
{{- end -}}

{{/*
Create the name of the service account to use
*/}}
{{- define "Turquoise.sync.serviceAccountName" -}}
{{- if .Values.serviceAccount.create -}}
    {{ default (include "Turquoise.sync.fullname" .) .Values.serviceAccount.name }}
{{- else -}}
    {{ default "default" .Values.serviceAccount.name }}
{{- end -}}
{{- end -}}
