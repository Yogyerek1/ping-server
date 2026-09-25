pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = 'ping'
        POSTGRES_DB = 'ping_db'
        POSTGRES_USER = 'postgres'
        ASPNETCORE_ENVIRONMENT = 'Production'
    }

    parameters {
        string(name: 'POSTGRES_HOST_PORT', defaultValue: '5432', description: 'Port mapped on the host machine for the PostgreSQL database.')
        string(name: 'API_HOST_PORT', defaultValue: '5276', description: 'Port mapped on the host machine for the Web API.')

        string(name: 'SMTP_HOST', defaultValue: 'smtp.gmail.com', description: 'SMTP Server Host')
        string(name: 'SMTP_PORT', defaultValue: '587', description: 'SMTP Server Port')
        string(name: 'SMTP_SENDER_NAME', defaultValue: 'Ping App', description: 'Sender Name')
        string(name: 'SMTP_SENDER_EMAIL', defaultValue: 'system@ping.com', description: 'Sender Email')
        string(name: 'SMTP_USERNAME', defaultValue: 'your-email@gmail.com', description: 'SMTP Username')
        string(name: 'SMTP_PASSWORD', defaultValue: '', description: 'SMTP password for the configured mail server.')

        string(name: 'NETWORK', defaultValue: '', description: 'Docker network name. If empty, Docker Compose uses the default network.')
        booleanParam(name: 'HAS_EXTERNAL_NETWORK', defaultValue: false, description: 'Check if the specified Docker network already exists on the host.')
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Prepare .env') {
            steps {
                withCredentials([
                    string(credentialsId: 'ping-db-password', variable: 'DB_PASSWORD'),
                    string(credentialsId: 'ping-smtp-password', variable: 'SMTP_PASS')
                ]) {
                    sh '''
                        if [ -n "${NETWORK}" ]; then
                            HAS_EXT="true"
                        else
                            HAS_EXT="false"
                        fi

                        cat > .env <<EOF
POSTGRES_DB=${POSTGRES_DB}
POSTGRES_USER=${POSTGRES_USER}
POSTGRES_PASSWORD=${DB_PASSWORD}
POSTGRES_HOST_PORT=${POSTGRES_HOST_PORT}
API_HOST_PORT=${API_HOST_PORT}
ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}

SMTP_HOST=${SMTP_HOST}
SMTP_PORT=${SMTP_PORT}
SMTP_SENDER_NAME=${SMTP_SENDER_NAME}
SMTP_SENDER_EMAIL=${SMTP_SENDER_EMAIL}
SMTP_USERNAME=${SMTP_USERNAME}
SMTP_PASSWORD=${SMTP_PASS}
SMTP_ENABLE_SSL=true

NETWORK=${NETWORK}
HAS_EXTERNAL_NETWORK=${HAS_EXT}
EOF
                    '''
                }
            }
        }

        stage('Build') {
            steps {
                sh 'docker compose build'
            }
        }

        stage('Deploy') {
            steps {
                sh 'docker compose up -d'
            }
        }

        stage('Cleanup') {
            steps {
                sh 'rm -f .env'
            }
        }
    }

    post {
        always {
            sh 'docker compose ps'
        }
        failure {
            sh 'rm -f .env || true'
        }
    }
}