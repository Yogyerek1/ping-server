pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = 'ping'
        POSTGRES_DB = 'ping_db'
        POSTGRES_USER = 'postgres'
        ASPNETCORE_ENVIRONMENT = 'Production'
    }

    parameters {
        string(name: 'POSTGRES_HOST_PORT', defaultValue: '5432', description: '')
        string(name: 'API_HOST_PORT', defaultValue: '5276', description: '')
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
                    string(credentialsId: 'ping-db-password', variable: 'DB_PASSWORD')
                ]) {
                    sh '''
                        cat > .env <<EOF
POSTGRES_DB=${POSTGRES_DB}
POSTGRES_USER=${POSTGRES_USER}
POSTGRES_PASSWORD=${DB_PASSWORD}
POSTGRES_HOST_PORT=${POSTGRES_HOST_PORT}
ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}
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